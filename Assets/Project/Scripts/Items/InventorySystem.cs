
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 20;

    [Header("Inventory Slots")]
    [SerializeField] private List<InventorySlot> slots =
        new List<InventorySlot>();

    public IReadOnlyList<InventorySlot> Slots => slots;

    public event Action OnInventoryChanged;

    private void Awake()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        inventorySize = Mathf.Max(1, inventorySize);

        while (slots.Count < inventorySize)
        {
            slots.Add(new InventorySlot());
        }

        if (slots.Count > inventorySize)
        {
            slots.RemoveRange(
                inventorySize,
                slots.Count - inventorySize
            );
        }
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0 || item.maxStack <= 0)
            return false;

        // Calculate available capacity.
        int availableSpace = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
                availableSpace += item.maxStack;
            else if (slot.item == item)
                availableSpace += slot.RemainingSpace();
        }

        // Reject if the inventory cannot fit everything.
        if (availableSpace < amount)
        {
            Debug.LogWarning("Not enough inventory space!");
            return false;
        }

        // Fill existing stacks first.
        foreach (InventorySlot slot in slots)
        {
            if (!slot.CanStack(item))
                continue;

            int added = Mathf.Min(
                amount,
                slot.RemainingSpace()
            );

            slot.quantity += added;
            amount -= added;

            if (amount <= 0)
                break;
        }

        // Fill empty slots.
        foreach (InventorySlot slot in slots)
        {
            if (amount <= 0)
                break;

            if (!slot.IsEmpty())
                continue;

            int added = Mathf.Min(amount, item.maxStack);

            slot.item = item;
            slot.quantity = added;
            amount -= added;
        }

        OnInventoryChanged?.Invoke();

        Debug.Log("Item added: " + item.itemName);

        return true;
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        // Check total quantity first.
        int total = GetItemCount(item);

        if (total < amount)
        {
            Debug.LogWarning("Not enough items to remove!");
            return false;
        }

        // Remove items from slots.
        foreach (InventorySlot slot in slots)
        {
            if (amount <= 0)
                break;

            if (slot.item != item)
                continue;

            int removed = Mathf.Min(
                amount,
                slot.quantity
            );

            slot.quantity -= removed;
            amount -= removed;

            if (slot.quantity <= 0)
                slot.Clear();
        }

        OnInventoryChanged?.Invoke();

        Debug.Log("Item removed: " + item.itemName);

        return true;
    }

    public int GetItemCount(ItemData item)
    {
        if (item == null)
            return 0;

        int total = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
                total += slot.quantity;
        }

        return total;
    }
}
