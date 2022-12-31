using System;
using System.Collections;
using UnityEngine;

namespace States
{
    public class PlayMoveItemsState : State<Core>
    {
        public PlayMoveItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            foreach (var slot in _core.slots)
            {
                slot.MoveItemToSlot();
            }
            _core.StartCoroutine(Delay(0.2f, () => ChangeState(new ShowHiddenItemsState(_core))));
        }
        
        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}