using System;
using System.Collections;
using UnityEngine;

namespace States
{
    public class MoveItemsState : State<Core>
    {
        public MoveItemsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("MoveItemsState");
            if (Model.firstGoalCount <= 0 && Model.secondGoalCount <= 0)
            {
                ChangeState(new VictoryState(_core)); 
            }
            else
            {
                foreach (var slot in _core.slots)
                {
                    slot.MoveItemToSlot();
                }
                _core.StartCoroutine(Delay(0.2f, () => ChangeState(new ShowHiddenItemsState(_core))));
            }
        }
        
        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}