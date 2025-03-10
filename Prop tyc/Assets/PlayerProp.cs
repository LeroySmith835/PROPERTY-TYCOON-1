using UnityEngine;
using System.Collections.Generic; // For storing player-owned properties

public class PlayerProp : MonoBehaviour
{
    public string playerName;
    public int playerID; // Unique identifier for each player (1 or 2)
    public int Money = 1500;
    
    public List<Property> ownedProperties = new List<Property>(); // Track owned properties

    public void PayRent(PlayerProp owner, int rentAmount)
    {
        if (Money >= rentAmount)
        {
            Money -= rentAmount;
            owner.Money += rentAmount;
            Debug.Log($"{playerName} paid £{rentAmount} to {owner.playerName}");
        }
        else
        {
            Debug.Log($"{playerName} is bankrupt and cannot pay rent!");
            HandleBankruptcy(owner, rentAmount);
        }
    }

    public void PurchaseProperty(Property property)
    {
        if (Money >= property.purchasePrice)
        {
            Money -= property.purchasePrice;
            property.owner = this;
            ownedProperties.Add(property);
            
            Debug.Log($"{playerName} purchased {property.propertyName}");
        }
        else
        {
            Debug.Log($"{playerName} does not have enough money to buy {property.propertyName}!");
        }
    }

    public void SellProperty(Property property)
    {
        if (ownedProperties.Contains(property))
        {
            Money += property.purchasePrice / 2; // Sell at half the original price
            property.owner = null;
            ownedProperties.Remove(property);
            Debug.Log($"{playerName} sold {property.propertyName} for £{property.purchasePrice / 2}");
        }
    }

    private void HandleBankruptcy(PlayerProp owner, int rentAmount)
    {
        if (ownedProperties.Count > 0)
        {
            Debug.Log($"{playerName} is selling properties to pay debt...");
            while (Money < rentAmount && ownedProperties.Count > 0)
            {
                SellProperty(ownedProperties[0]); // Sell properties until debt is paid
            }

            if (Money >= rentAmount)
            {
                Money -= rentAmount;
                owner.Money += rentAmount;
                Debug.Log($"{playerName} paid off debt after selling properties.");
            }
            else
            {
                Debug.Log($"{playerName} is still bankrupt and has lost the game!");
                GameManager.Instance.PlayerBankrupt(this); // Handle elimination
            }
        }
        else
        {
            Debug.Log($"{playerName} has no properties left and is eliminated!");
            GameManager.Instance.PlayerBankrupt(this);
        }
    }
}
