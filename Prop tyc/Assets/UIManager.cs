using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public GameObject purchaseUI; // Assign in Inspector
    public Button buyButton; // Assign Buy Button in Inspector
    public Button ignoreButton; // Assign Ignore Button in Inspector

    private Property currentProperty; // Store the property for purchase
    private PlayerProp currentPlayer; // Store the player attempting to buy

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPurchaseOption(Property property, PlayerProp player)
    {
        Debug.Log($"{player.playerName} landed on {property.propertyName}. Buy for £{property.purchasePrice}?");
        
        currentProperty = property;
        currentPlayer = player;

        // Enable the purchase UI
        if (purchaseUI != null)
        {
            purchaseUI.SetActive(true);
        }

        // Assign button actions dynamically
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners(); // Clear previous listeners
            buyButton.onClick.AddListener(() => BuyProperty());
        }

        if (ignoreButton != null)
        {
            ignoreButton.onClick.RemoveAllListeners(); // Clear previous listeners
            ignoreButton.onClick.AddListener(() => IgnorePurchase());
        }
    }

    private void BuyProperty()
    {
        if (currentProperty != null && currentPlayer != null)
        {
            currentProperty.PurchaseProperty(currentPlayer);
            ClosePurchaseUI();
        }
    }

    private void IgnorePurchase()
    {
        Debug.Log($"{currentPlayer.playerName} ignored the purchase.");
        ClosePurchaseUI();
    }

    private void ClosePurchaseUI()
    {
        if (purchaseUI != null)
        {
            purchaseUI.SetActive(false);
        }
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
