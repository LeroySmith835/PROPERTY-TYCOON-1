using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PropertySetManager : MonoBehaviour
{
    [System.Serializable]
    public class PropertySet
    {
        public string setName; // Example: "Brown Set"
        public Property[] properties; // Properties in this set
        public int upgradeCost;
        public int maxUpgradeLevel = 5;
        public Image[] upgradeIcons; // Images representing each upgrade level
        public PlayerProp owner;
        public int currentUpgradeLevel = 0;
    }

    public List<PropertySet> propertySets = new List<PropertySet>(); // List of all property sets

    public void CheckFullOwnership(Property property)
    {
        foreach (PropertySet set in propertySets)
        {
            if (System.Array.Exists(set.properties, p => p == property))
            {
                PlayerProp possibleOwner = set.properties[0].owner;
                if (possibleOwner == null) return;

                foreach (Property p in set.properties)
                {
                    if (p.owner != possibleOwner) return; // Not fully owned
                }

                set.owner = possibleOwner;
                Debug.Log($"{set.owner.playerName} owns the full {set.setName} and can upgrade!");

                UIManager.Instance.ShowMessage($"{set.owner.playerName} owns all {set.setName} properties!");
            }
        }
    }

    public void UpgradePropertySet(string setName)
    {
        PropertySet set = propertySets.Find(s => s.setName == setName);
        if (set == null || set.owner == null || set.currentUpgradeLevel >= set.maxUpgradeLevel) return;

        if (set.owner.Money >= set.upgradeCost)
        {
            set.owner.Money -= set.upgradeCost;
            set.currentUpgradeLevel++;

            Debug.Log($"{set.owner.playerName} upgraded {set.setName} to level {set.currentUpgradeLevel}");
            UIManager.Instance.ShowMessage($"{set.owner.playerName} upgraded {set.setName}!");

            UpdateUpgradeVisual(set);
        }
        else
        {
            UIManager.Instance.ShowMessage("Not enough money to upgrade!");
        }
    }

    private void UpdateUpgradeVisual(PropertySet set)
    {
        for (int i = 0; i < set.upgradeIcons.Length; i++)
        {
            set.upgradeIcons[i].gameObject.SetActive(i == set.currentUpgradeLevel - 1);
        }
    }
}
