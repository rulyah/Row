using UnityEngine;

namespace StateMachine
{
    public class HideItemState : State<Game>
    {
        public HideItemState(Game core) : base(core) { }

        public override void OnEnter()
        {
            Debug.Log("HideCornerCellState");

            _core.HideItem(1,1);
            _core.HideItem(8, 1);
            _core.HideItem(1, 8);
            _core.HideItem(8,8);
            ChangeState(new CheckGridState(_core));
        }
    }
}