
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private ItemUsageSystem itemUsage;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private InventorySlotUI slotPrefab;

    private InventorySlotUI[] slotUI;

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnInventoryChanged += RefreshInventory;
        }

        RefreshInventory();
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnInventoryChanged -= RefreshInventory;
        }
    }

    private void Start()
    {
        if (inventory == null ||
            itemUsage == null ||
            slotContainer == null ||
            slotPrefab == null)
        {
            Debug.LogError(
                "InventoryUI references are incomplete!"
            );
            return;
        }

        CreateSlots();
        RefreshInventory();
    }

    private void CreateSlots()
    {
        int slotCount = inventory.Slots.Count;

        slotUI = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            slotUI[i] = Instantiate(
                slotPrefab,
                slotContainer
            );

            slotUI[i].ClearSlot();
        }

        Debug.Log("Created " + slotCount + " inventory slots.");
    }

    public void RefreshInventory()
    {
        Debug.Log("INVENTORY UI REFRESH CALLED");

    if (slotUI == null || inventory == null)
    {
        Debug.LogWarning(
            "Inventory UI belum siap atau Inventory kosong!"
        );
        return;
    }

    Debug.Log("Inventory ID: " + inventory.GetEntityId());

    for (int i = 0; i < slotUI.Length; i++)
    {
        slotUI[i].UpdateSlot(
            inventory.Slots[i],
            itemUsage
        );
    }
    Debug.Log("InventoryUI ID: " + inventory.GetEntityId());
}
}
