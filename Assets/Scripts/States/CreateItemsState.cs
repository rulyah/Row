using Configs;
using UnityEngine;

namespace States
{
    public class CreateItemsState : State<Core>
    {
        public CreateItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            for (var i = 0; i < _core.slots.Count; i++)
            {
                var item = Factory.CreateItem(GameConfig.itemPrefab);
                item.SetParent(_core.slots[i]);
                item.transform.localPosition = Vector3.zero;
            }
            ChangeState(new SetLevelTaskState(_core));
        }
    }
}