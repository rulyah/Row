using Configs;

namespace States
{
    public class InputItemsValueState : State<Core>
    {
        public InputItemsValueState(Core core) : base(core) {}

        public override void OnEnter()
        {
            for (var i = 0; i < _core.slots.Count; i++)
            {
                _core.slots[i].itemInSlot.spriteId = Model.levelModel.currentLevelConfig.spriteId[i];
                _core.slots[i].itemInSlot.image.sprite = GameConfig.spritesConfig.sprites[_core.slots[i].itemInSlot.spriteId];
            }
            ChangeState(new HideCornerState(_core));
        }
    }
}