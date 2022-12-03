using Configs;
using UnityEngine;

namespace States
{
    public class InputItemsValueState : State<Core>
    {
        public InputItemsValueState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("InputItemsValueState");

            for (var i = 0; i < _core.slots.Count; i++)
            {
                _core.slots[i].itemInSlot.spriteId = GameConfig._currentLevelConfig.spriteId[i];
                _core.slots[i].itemInSlot.image.sprite = _core.sprites[_core.slots[i].itemInSlot.spriteId];
            }
            ChangeState(new HideCornerState(_core));
        }
    }
}