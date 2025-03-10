using UnityEngine;
using UnityEngine.UI; // For handling UI elements

public class Property : MonoBehaviour
{
    public string propertyName;
    public int purchasePrice;
    public int rentPrice;
    public PlayerProp owner;

    public Image ownershipIcon; // Assign this in the Unity Inspector
    public Sprite player1Icon;  // Icon when Player 1 owns it
    public Sprite player2Icon;  // Icon when Player 2 owns it

    private PropertySetManager propertySetManager;

    private void Start()
    {
        propertySetManager = FindObjectOfType<PropertySetManager>();
        if (propertySetManager == null)
        {
            Debug.LogError("PropertySetManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerProp player = collision.GetComponent<PlayerProp>(); // Check if a player entered
        if (player != null) // Make sure it's a player
        {
            Debug.Log($"{player.playerName} landed on {propertyName}");
            
            if (owner == null)
            {
                UIManager.Instance.ShowPurchaseOption(this, player);
            }
            else if (owner != player)
            {
                player.PayRent(owner, rentPrice);
            }
        }
    }

    public void PurchaseProperty(PlayerProp player)
    {
        if (owner == null && player.Money >= purchasePrice)
        {
            player.Money -= purchasePrice;
            owner = player;
            Debug.Log($"{player.playerName} purchased {propertyName}");
            UIManager.Instance.ShowMessage($"{player.playerName} purchased {propertyName}");

            UpdateOwnershipVisual(player);
            propertySetManager?.CheckFullOwnership(this); // Check if player owns a full set
        }
        else
        {
            UIManager.Instance.ShowMessage($"Not enough money to purchase {propertyName}");
        }
    }

    private void UpdateOwnershipVisual(PlayerProp player)
    {
        if (ownershipIcon != null)
        {
            // Set player-specific ownership icon
            if (player.playerID == 1)
                ownershipIcon.sprite = player1Icon;
            else if (player.playerID == 2)
                ownershipIcon.sprite = player2Icon;

            ownershipIcon.gameObject.SetActive(true); // Show the icon
        }
    }

    public void UpgradeProperty(int upgradeLevel, Sprite upgradeIcon)
    {
        rentPrice += (rentPrice / 2) * upgradeLevel; // Increase rent for upgrades
        if (ownershipIcon != null)
        {
            ownershipIcon.sprite = upgradeIcon; // Update to upgraded visual
        }
    }
}
