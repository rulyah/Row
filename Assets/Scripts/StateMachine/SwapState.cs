using System;
using UnityEngine;

namespace StateMachine
{
    public class SwapState : State<Game>
    {
        public SwapState(Game core) : base(core) { }

        public static event Action<int> onCellsMove;
        public override void OnEnter()
        {
            //_core.movesCount++;
            Debug.Log("SwapState");
            _core.CellSwap(_core.firstCell, _core.secondCell);
            
            _core.PlayDelay(0.3f,() =>
            {
                ChangeState(new CheckGridState(_core));
            });
            
        }
    }
}