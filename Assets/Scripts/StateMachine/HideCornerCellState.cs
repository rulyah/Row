using UnityEngine;

namespace StateMachine
{
    public class HideCornerCellState : State<Game>
    {
        public HideCornerCellState(Game core) : base(core) { }

        public override void OnEnter()
        {
            Debug.Log("HideCornerCellState");

            _core.HideCell(1,1);
            _core.HideCell(8, 1);
            _core.HideCell(1, 8);
            _core.HideCell(8,8);
            ChangeState(new CheckGridState(_core));
        }
    }
}