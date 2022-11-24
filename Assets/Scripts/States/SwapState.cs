using System;
using System.Collections;
using Configs;
using UnityEngine;

namespace States
{
    public class SwapState : State<Core>
    {
        public SwapState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("SwapState");
            var item = _core.model.firstSlot.itemInSlot;
            _core.model.secondSlot.itemInSlot.Move(_core.model.firstSlot);
            item.Move(_core.model.secondSlot);
            if (GameConfig.isWrongMove)
            {
                GameConfig.isWrongMove = false;
                _core.StartCoroutine(Delay(0.5f, () => ChangeState(new InputState(_core))));
            }
            else
            {
                _core.StartCoroutine(Delay(0.5f, () => ChangeState(new CheckGridState(_core))));
            }
        }
        
        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}