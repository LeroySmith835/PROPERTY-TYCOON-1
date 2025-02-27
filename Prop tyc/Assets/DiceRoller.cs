using UnityEngine;
using UnityEngine.UI;
using System;

public class DiceRoller : MonoBehaviour
{
    public int[] diceValues = new int[2]; // Stores dice values
    public event Action<int> OnDiceRolled; // Event for dice roll outcome

    public Sprite[] diceImages; // Holds dice face images (1-6)
    public Image dice1Image; // UI Image for Dice 1
    public Image dice2Image; // UI Image for Dice 2

    public void RollDice()
    {
        // Roll two dice
        diceValues[0] = UnityEngine.Random.Range(1, 7); // Dice 1 (1-6)
        diceValues[1] = UnityEngine.Random.Range(1, 7); // Dice 2 (1-6)

        int sum = diceValues[0] + diceValues[1]; // Sum the dice values
        OnDiceRolled?.Invoke(sum); // Notify listeners

        UpdateDiceVisuals(); // Update UI visuals
    }

    void UpdateDiceVisuals()
    {
        // Ensure both images and dice sprites are assigned
        if (dice1Image != null && dice2Image != null && diceImages.Length == 6)
        {
            dice1Image.sprite = diceImages[diceValues[0] - 1]; // Update Dice 1
            dice2Image.sprite = diceImages[diceValues[1] - 1]; // Update Dice 2
        }
        else
        {
            Debug.LogError("Dice images or UI Image components are not properly assigned!");
        }
    }

    public void TriggerDiceRoll()
    {
        RollDice();
    }
}
