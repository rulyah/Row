using UnityEngine;

namespace StateMachine
{
    public class SwapBeckState : State<Game>
    {
        public SwapBeckState(Game core) : base(core) { }
        
        public override void OnEnter()
        {
            Debug.Log("SwapBeckState");

            if (_core.firstCell != null)
            {
                _core.ItemSwap(_core.firstCell, _core.secondCell);
                _core.PlayDelay(0.2f,() =>
                {
                    ChangeState(new InputState(_core));
                });
            }
            else
            {
                ChangeState(new InputState(_core));
            }
        }
    }
}