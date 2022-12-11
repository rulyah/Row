using Commands;
using UnityEngine;

namespace States
{
    public class AddItemsState : State<Core>
    {
        public AddItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            var slots = _core.slots.FindAll(n => n.itemInSlot == null);
            for (var i = 0; i < slots.Count; i++)
            {
                SetItemCommand.SetItem(slots[i],Factory.SetItem());
                slots[i].itemInSlot.Show();
                slots[i].itemInSlot.transform.localScale = Vector3.one;

            }
            ChangeState(new MoveItemsState(_core));
        }
    }
}