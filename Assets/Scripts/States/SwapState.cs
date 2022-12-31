using System;
using System.Collections;
using UnityEngine;

namespace States
{
    public class SwapState : State<Core>
    {
        public SwapState(Core core) : base(core) {}

        public override void OnEnter()
        {
            var item = Model.levelModel.firstSlot.itemInSlot;
            Model.levelModel.secondSlot.itemInSlot.Move(Model.levelModel.firstSlot);
            item.Move(Model.levelModel.secondSlot);
            
            if (Model.levelModel.isWrongMove)
            {
                Model.levelModel.isWrongMove = false;
                _core.StartCoroutine(Delay(0.5f, () => ChangeState(new InputState(_core))));
            }
            else _core.StartCoroutine(Delay(0.5f, () => ChangeState(new CheckGridState(_core))));
        }

        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}