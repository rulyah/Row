using UnityEngine;

namespace Commands
{
    public static class CreateItemCommand
    {
        public static void CreateItem(Core core, Slot slot)
        {
            var item = core.fabric.CreateItem();
            item.spriteId = core.model.currentConfig.spriteId[core.slots.IndexOf(slot)];
            item.image.sprite = core.sprites[item.spriteId];
            item.SetParent(slot);
            item.transform.localPosition = Vector3.zero;
            slot.SetItem(item);
        }
    }
}