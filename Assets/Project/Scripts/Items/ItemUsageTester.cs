
using UnityEngine;

public class ItemUsageTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ItemUsageSystem itemUsage;
    [SerializeField] private ItemData healthPotion;
    [SerializeField] private ItemData manaPotion;

    [ContextMenu("Test/Use Health Potion")]
    private void TestHealthPotion()
    {
        itemUsage.UseItem(healthPotion);
    }

    [ContextMenu("Test/Use Mana Potion")]
    private void TestManaPotion()
    {
        itemUsage.UseItem(manaPotion);
    }
}
