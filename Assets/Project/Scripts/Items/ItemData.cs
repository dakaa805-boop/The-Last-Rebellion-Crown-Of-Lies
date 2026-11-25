
using UnityEngine;

public enum ItemType
{
    Consumable,
    Material,
    Equipment,
    Special
}

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "Crown of Ashes/Item Data"
)]
public class ItemData : ScriptableObject
{
    [Header("Basic Information")]
    public string itemID;
    public string itemName;
    [TextArea(3, 5)]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Item Settings")]
    public ItemType itemType;
    public int maxStack = 99;
}
