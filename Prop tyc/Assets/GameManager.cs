using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public List<PlayerProp> players = new List<PlayerProp>(); // List of active players
    public int currentPlayerIndex = 0; // Tracks whose turn it is

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Find and store all PlayerProp objects in the scene
        players.AddRange(FindObjectsOfType<PlayerProp>());

        if (players.Count < 2)
        {
            Debug.LogError("Not enough players! Ensure you have at least 2 PlayerProp objects in the scene.");
        }
    }

    public PlayerProp GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public void NextTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        Debug.Log($"It's now {players[currentPlayerIndex].playerName}'s turn!");
    }

    public void PlayerBankrupt(PlayerProp player)
    {
        Debug.Log($"{player.playerName} is bankrupt and out of the game!");

        // Remove player from the game
        players.Remove(player);
        Destroy(player.gameObject);

        // Check for a winner
        if (players.Count == 1)
        {
            Debug.Log($"{players[0].playerName} wins the game!");
        }
        else
        {
            NextTurn(); // Continue playing
        }
    }
}
