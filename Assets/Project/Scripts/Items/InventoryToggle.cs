
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("Settings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.I;

    private void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
