using UnityEngine;

namespace StateMachine
{
    public class CheckCornerCellState : State<Game>
    {
        public CheckCornerCellState(Game core) : base(core) { }

        public override void OnEnter()
        {
            Debug.Log("CheckCornerCellState");

            for (var i = 0; i < _core.matchList.Count; i++)
            {
                if (_core.matchList[i].posX is 1 or 8)
                {
                    ShowCell(1, 8); 
                    ShowCell(8, 8); 
                }
            }
            ChangeState(new MoveItemsState(_core));
        }

        private void ShowCell(int posX, int posY)
        {
            var cell = _core.cells.Find(n => n.posX == posX && n.posY == posY);
            cell.itemInCell.Show();
        }
    }
}