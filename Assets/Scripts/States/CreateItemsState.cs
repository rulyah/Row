using Configs;
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
                var item = _core.factory.CreateItem();
                item.SetParent(_core.slots[i]);
                item.transform.localPosition = Vector3.zero;
                _core.slots[i].SetItem(item);
            }
            ChangeState(new SetLevelTaskState(_core, GameConfig.currentLevel));
        }
    }
}