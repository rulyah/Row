using Commands;
using UnityEngine;

namespace States
{
    public class CreateItemsState : State<Core>
    {
        public CreateItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("CreateItemsState");
            for (var i = 0; i < _core.slots.Count; i++)
            {
                CreateItemCommand.CreateItem(_core, _core.slots[i]);
                /*var item = _core.fabric.CreateItem();
                item.spriteId = _core.model.currentConfig.spriteId[i];
                item.image.sprite = _core.sprites[item.spriteId];
                item.SetParent(_core.slots[i]);
                item.transform.localPosition = Vector3.zero;
                _core.slots[i].RefreshItem();*/
            }
            ChangeState(new HideCornerState(_core));
        }
    }
}