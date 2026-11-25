
using UnityEngine;

public class ItemUsageSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMagic playerMagic;

    public bool UseItem(ItemData item)
    {
        if (item == null || inventory == null)
            return false;

        if (inventory.GetItemCount(item) <= 0)
        {
            Debug.LogWarning("Item tidak tersedia!");
            return false;
        }

        switch (item.itemID)
        {
            case "consumable_health_potion":
                if (playerHealth == null)
                    return false;

                if (playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    Debug.Log("HP masih penuh!");
                    return false;
                }

                playerHealth.Heal(50);
                break;

            case "consumable_mana_potion":
                if (playerMagic == null)
                    return false;

                if (playerMagic.CurrentMana >= playerMagic.MaxMana)
                {
                    Debug.Log("Mana masih penuh!");
                    return false;
                }

                playerMagic.RestoreMana(40);
                break;

            default:
                Debug.Log("Item ini belum bisa digunakan.");
                return false;
        }

        inventory.RemoveItem(item, 1);

        Debug.Log("Item digunakan: " + item.itemName);
        return true;
    }
}
