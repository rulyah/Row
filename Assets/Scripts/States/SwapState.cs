using System;
using System.Collections;
using Commands;
using UnityEngine;

namespace States
{
    public class SwapState : State<Core>
    {
        public SwapState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("SwapState");
            SwapItemCommand.SwapItem(_core.model.firstSlot, _core.model.secondSlot);
            _core.StartCoroutine(Delay(0.5f, () => ChangeState(new CheckGridState(_core))));
        }
        
        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}