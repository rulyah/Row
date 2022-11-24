using Configs;
using UnityEngine;

namespace States
{
    public class HideCornerState : State<Core>
    {
        public HideCornerState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("HideCornerState");
            HideSlot(1,1);
            HideSlot(1,8);
            HideSlot(8,8);
            HideSlot(8,1);
            
            if (GameConfig.isNeedToCheckGrid)
            {
                GameConfig.isNeedToCheckGrid = false;
                ChangeState(new CheckGridState(_core));
            }
            else
            {
                ChangeState(new InputState(_core));
            }
        }

        private void HideSlot(int posX, int posY)
        {
            var slot = _core.slots.Find(n => n.posX == posX && n.posY == posY);
            slot.SetNonInteractable();
            slot.itemInSlot.HideItem();
        }
    }
}