using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject purchaseUI; // Assign in Inspector

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPurchaseOption(Property property, PlayerProp player)
    {
        Debug.Log($"{player.playerName} landed on {property.propertyName}. Buy for £{property.purchasePrice}?");
        
        // Enable the purchase UI
        if (purchaseUI != null)
        {
            purchaseUI.SetActive(true);
        }

        // TODO: Implement UI buttons for Yes/No purchase decision
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
