using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPurchaseOption(Property property, PlayerProp player)
    {
        Debug.Log(player.playerName + " landed on " + property.propertyName + ". Buy for " + property.purchasePrice + "?");
        // Implement UI buttons for Yes/No purchase options
        // Example of button clicks that call `property.PurchaseProperty(player)` when the user decides to buy
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
