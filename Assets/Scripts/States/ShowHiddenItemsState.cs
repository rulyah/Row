using System.Linq;
using Configs;
using UnityEngine;

namespace States
{
    public class ShowHiddenItemsState : State<Core>
    {
        public ShowHiddenItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("ShowHiddenItemsState");
            foreach (var slot in _core.slots.Where(slot => slot.itemInSlot.isHide))
            {
                slot.itemInSlot.Show();
                slot.itemInSlot.transform.localScale = Vector3.one;
            }
            GameConfig.isNeedToCheckGrid = true;
            ChangeState(new HideCornerState(_core));
        }
    }
}