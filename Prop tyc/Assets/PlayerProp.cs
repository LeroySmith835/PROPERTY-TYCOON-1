using UnityEngine;

public class PlayerProp : MonoBehaviour
{
    public string playerName;
    public int Money = 1500;

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
        }
    }
}
