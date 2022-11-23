using UnityEngine;

namespace Commands
{
    public static class SwapItemCommand
    {
        public static void SwapItem(Slot first, Slot second)
        {
            Debug.Log("swap");
            var item = first.itemInSlot;
            second.itemInSlot.Move(first);
            item.Move(second);
        }
    }
}