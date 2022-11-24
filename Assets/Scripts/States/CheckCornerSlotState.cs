using System.Linq;
using UnityEngine;

namespace States
{
    public class CheckCornerSlotState : State<Core>
    {
        public CheckCornerSlotState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("CheckCornerSlotState");

            if (Model.matchList.Any(t => t.posX is 1 or 8))
            {
                ShowItem(1, 8);
                ShowItem(8, 8);
            }
            ChangeState(new RemoveMatchItemsState(_core));
        }
        
        private void ShowItem(int posX, int posY)
        {
            var slot = _core.slots.Find(n => n.posX == posX && n.posY == posY);
            slot.itemInSlot.Show();
        }
    }
}