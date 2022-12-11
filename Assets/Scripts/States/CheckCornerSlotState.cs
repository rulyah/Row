using System.Linq;
using UI;
using UnityEngine;

namespace States
{
    public class CheckCornerSlotState : State<Core>
    {
        public CheckCornerSlotState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("CheckCornerSlotState");

            if (Model.levelModel.matchList.Any(n => n.posX == 1))
            {
                ShowItem(1, 8);
            }
            if (Model.levelModel.matchList.Any(n => n.posX == 8))
            {
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