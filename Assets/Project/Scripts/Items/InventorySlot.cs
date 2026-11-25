
using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty()
    {
        return item == null || quantity <= 0;
    }

    public bool CanStack(ItemData newItem)
    {
        return !IsEmpty() && item == newItem &&
               quantity < item.maxStack;
    }

    public int RemainingSpace()
    {
        if (IsEmpty()) return 0;

        return Mathf.Max(0, item.maxStack - quantity);
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
