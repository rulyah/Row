using System;
using System.Collections;
using Commands;
using UnityEngine;

namespace States
{
    public class SwapBeckState : State<Core>
    {
        public SwapBeckState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("SwapBeckState");

            if (_core.model.firstSlot != null)
            {
                SwapItemCommand.SwapItem(_core.model.firstSlot, _core.model.secondSlot);
                _core.StartCoroutine(Delay(0.5f,() => ChangeState(new InputState(_core))));
            }
            else
            {
                ChangeState(new InputState(_core));
            }
        }

        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}