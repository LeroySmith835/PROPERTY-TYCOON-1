using UnityEngine;

public class Property : MonoBehaviour
{
    public string propertyName;
    public int purchasePrice;
    public int rentPrice;
    public PlayerProp owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerProp player = collision.GetComponent<PlayerProp>(); // Check if a player entered
        if (player != null) // Make sure it's a player
        {
            Debug.Log($"{player.playerName} landed on {propertyName}");
            UIManager.Instance.ShowPurchaseOption(this, player);
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
        }
        else
        {
            UIManager.Instance.ShowMessage($"Not enough money to purchase {propertyName}");
        }
    }
}
