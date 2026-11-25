
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Button slotButton;

    private ItemData currentItem;
    private ItemUsageSystem itemUsage;

    private void Awake()
    {
        if (slotButton != null)
        {
            slotButton.onClick.AddListener(UseCurrentItem);
        }
    }

    public void UpdateSlot(
        InventorySlot slot,
        ItemUsageSystem usageSystem)
    {
        itemUsage = usageSystem;

        if (slot == null || slot.IsEmpty())
        {
            ClearSlot();
            return;
        }

        currentItem = slot.item;

        if (itemIcon != null)
        {
            itemIcon.sprite = currentItem.icon;
            itemIcon.enabled = currentItem.icon != null;
        }

       if (quantityText != null)
        {
            quantityText.text = slot.quantity.ToString();
            quantityText.enabled = true;
        }
    }

    private void UseCurrentItem()
    {
        if (currentItem == null || itemUsage == null)
            return;

        itemUsage.UseItem(currentItem);
    }

    public void ClearSlot()
    {
        currentItem = null;

        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        if (quantityText != null)
        {
            quantityText.text = "";
            quantityText.enabled = false;
        }
    }
}
