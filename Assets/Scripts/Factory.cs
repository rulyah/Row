using UnityEngine;

public static class Factory
{
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
        var item = Model.levelModel.hiddenItems[Random.Range(0, Model.levelModel.hiddenItems.Count)];
        Model.levelModel.hiddenItems.Remove(item);
        return item;
    }

    public static void RemoveItem(Item item)
    {
        Model.levelModel.hiddenItems.Add(item);
        item.HideItem();
    }
}