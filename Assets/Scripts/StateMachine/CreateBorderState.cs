using UnityEngine;

namespace StateMachine
{
    public class CreateBorderState : State<Game>
    {

        public CreateBorderState(Game core) : base(core) { }


        public override void OnEnter()
        {
            Debug.Log("CreateBorderState");
            for (var x = 1; x <= _core.gridSize; x++)
            {
                for (var y = 1; y <= _core.gridSize + 5; y++)
                {
                    _core.CreateCell(x, y);
                }
            }
            ChangeState(new HideItemState(_core));
        }
    }
}