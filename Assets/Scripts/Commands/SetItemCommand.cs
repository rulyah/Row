using UnityEngine;

namespace Commands
{
    public static class SetItemCommand
    {
        public static void SetItem(Slot slot, Item item)
        {
            item.SetParent(slot);
            item.transform.localPosition = Vector3.zero;
            slot.SetItem(item);
        }
    }
}