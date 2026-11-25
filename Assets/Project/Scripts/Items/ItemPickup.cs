
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private ItemData itemData;
    [SerializeField] private int quantity = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        InventorySystem inventory =
            other.GetComponent<InventorySystem>();

        if (inventory == null)
        {
            Debug.LogWarning("Player has no InventorySystem!");
            return;
        }

        bool success = inventory.AddItem(itemData, quantity);
        
        if (success)
        {
            Debug.Log("Picked up: " + itemData.itemName);
            Destroy(gameObject);
        }
        Debug.Log("Inventory ID: " + inventory.GetEntityId());
    }
}
