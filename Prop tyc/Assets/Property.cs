using UnityEngine;

public class Property : MonoBehaviour
{
    public string propertyName;
    public int purchasePrice;
    public int rentPrice;
    public PlayerProp owner;  // Owner is now of type PlayerProp

    public void OnPlayerLand(PlayerProp player)
    {
        if (owner == null)
        {
            // Offer to buy the property if it's not owned
            UIManager.Instance.ShowPurchaseOption(this, player);
        }
        else if (owner != player)
        {
            // Charge rent if another player lands on it
            player.PayRent(owner, rentPrice);
        }
    }

    public void PurchaseProperty(PlayerProp player)
    {
        if (owner == null) // Ensure property can only be purchased if it's not owned
        {
            if (player.Money >= purchasePrice)
            {
                player.Money -= purchasePrice; // Deduct money from player
                owner = player; // Assign the player as the new owner
                UIManager.Instance.ShowMessage(player.playerName + " purchased " + propertyName);
            }
            else
            {
                UIManager.Instance.ShowMessage(player.playerName + " does not have enough money to purchase " + propertyName);
            }
        }
        else
        {
            UIManager.Instance.ShowMessage(propertyName + " is already owned by " + owner.playerName);
        }
    }
}
