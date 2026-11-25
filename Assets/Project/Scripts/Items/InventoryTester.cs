
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private ItemData healthPotion;

    [ContextMenu("Test/Add 5 Health Potions")]
    private void TestAddItem()
    {
        bool success = inventory.AddItem(healthPotion, 5);

        Debug.Log("Add Item Success: " + success);
        Debug.Log("Health Potion Count: " +
                  inventory.GetItemCount(healthPotion));
    }

    [ContextMenu("Test/Remove 2 Health Potions")]
    private void TestRemoveItem()
    {
        bool success = inventory.RemoveItem(healthPotion, 2);

        Debug.Log("Remove Item Success: " + success);
        Debug.Log("Health Potion Count: " +
                  inventory.GetItemCount(healthPotion));
    }

    [ContextMenu("Test/Check Health Potion")]
    private void TestGetItemCount()
    {
        Debug.Log("Current Health Potion: " +
                  inventory.GetItemCount(healthPotion));
    }
}
