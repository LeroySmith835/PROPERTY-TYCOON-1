using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import UI for Button
using System.Collections.Generic;


public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints for movement
    public float moveSpeed = 5f;
    public float waitTime = 0.5f;

    public Button rollButton; // Reference to Roll Button
    private DiceRoller diceRoller; // Reference to DiceRoller
    private bool isMoving = false; // Flag for movement status

    private Dictionary<PlayerProp, int> playerWaypointIndices = new Dictionary<PlayerProp, int>(); // Tracks each player's position

    void Start()
    {
        diceRoller = FindObjectOfType<DiceRoller>(); // Find DiceRoller in scene
        if (diceRoller != null)
        {
            diceRoller.OnDiceRolled += StartMoving;
        }
        else
        {
            Debug.LogError("DiceRoller not found in the scene!");
        }

        if (rollButton == null)
        {
            Debug.LogError("Roll Button is not assigned!");
        }

        // Initialize player waypoint indices
        foreach (PlayerProp player in GameManager.Instance.players)
        {
            playerWaypointIndices[player] = 0; // Start at position 0
        }
    }

    private void StartMoving(int diceSum)
    {
        if (!isMoving)
        {
            DisableRollButton(); // Disable roll button when movement starts

            PlayerProp currentPlayer = GameManager.Instance.GetCurrentPlayer();
            if (currentPlayer != null && playerWaypointIndices.ContainsKey(currentPlayer))
            {
                StartCoroutine(MoveToWaypoints(currentPlayer, playerWaypointIndices[currentPlayer], diceSum));
            }
        }
    }

    IEnumerator MoveToWaypoints(PlayerProp player, int waypointIndex, int steps)
    {
        isMoving = true;

        // Disable the player's BoxCollider2D while moving
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Move player step by step to the target waypoint based on the dice roll
        for (int i = 0; i < steps; i++)
        {
            waypointIndex++; // Increment to move to the next waypoint

            if (waypointIndex >= waypoints.Length) break; // Check if beyond last waypoint

            Transform targetWaypoint = waypoints[waypointIndex];

            // Move towards the target waypoint
            while (Vector3.Distance(player.transform.position, targetWaypoint.position) > 0.01f)
            {
                player.transform.position = Vector3.Lerp(player.transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime); // Wait at the waypoint
        }

        // Update the player's waypoint index after moving
        playerWaypointIndices[player] = waypointIndex;

        // Re-enable the player's BoxCollider2D after reaching the final waypoint
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isMoving = false; // Allow next move

        EnableRollButton(); // Enable roll button after movement
        GameManager.Instance.NextTurn(); // Let GameManager handle turn switching
    }

    void DisableRollButton()
    {
        if (rollButton != null)
        {
            rollButton.interactable = false;
        }
    }

    void EnableRollButton()
    {
        if (rollButton != null)
        {
            rollButton.interactable = true;
        }
    }
}
