using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    private static readonly List<Item> _items = new List<Item>();

    public static Slot CreateSlot(Slot slotPrefab)
    {
        return Object.Instantiate(slotPrefab);
    }

    public static Item CreateItem(Item itemPrefab)
    {
        return Object.Instantiate(itemPrefab);
    }

    public static Item SetItem()
    {
        var item = _items[Random.Range(0, _items.Count)];
        _items.Remove(item);
        return item;
    }

    public static void RemoveItemInSlot(Slot slot)
    {
        _items.Add(slot.itemInSlot);
        slot.RemoveItem();
        Debug.Log(_items.Count.ToString());
    }
}