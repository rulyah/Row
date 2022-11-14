using UnityEngine;

namespace StateMachine
{
    public class CheckGridState : State<Game>
    {
        public CheckGridState(Game core) : base(core) { }

        public override void OnEnter()
        {
            Debug.Log("CheckGridState");

            CheckLine();
            CheckColumn();
            
            if (_core.matchList.Count < 3)
            {
                ChangeState(new InputState(_core));
                //ChangeState(new SwapBeckState(_core));
            }
            else
            {
                ChangeState(new CheckCornerCellState(_core));
            }
        }

        private void CheckLine()
        {
            for (var y = 1; y <= _core.gridSize; y++)
            {
                //Debug.Log($"chek {y} line");
                var _cells = _core.cells.FindAll(n => n.posY == y);
                for (var x = 0; x < _cells.Count - 1; x++)
                {
                    //Debug.Log($"Check {x+1} pos");
                    var next = x + 1;
                    //Debug.Log($"sprite 1 - {x+1} + {y} - {_cells[x].itemInCell.spriteId}");
                    //Debug.Log($"sprite next {next+1} + {y} - {_cells[next].itemInCell.spriteId}");

                    if (_cells[x].itemInCell.spriteId == _cells[next].itemInCell.spriteId)
                    {
                        if(!_core.checkedCells.Contains(_cells[x])) _core.checkedCells.Add(_cells[x]);
                        //Debug.Log($"sovpalo, {x+1} + {y}  ++++ {next+1} + {y}");
                        if(!_core.checkedCells.Contains(_cells[next])) _core.checkedCells.Add(_cells[next]);
                        //Debug.Log($"{_core.checkedCells.Count} in list checked");
                    }
                    else
                    {
                        //Debug.Log($"Ne sovpalo {_core.checkedCells.Count} in list");
                        if(_core.checkedCells.Count < 3) _core.checkedCells.Clear();
                        else
                        {
                            for (var i = 0; i < _core.checkedCells.Count; i++)
                            {
                                _core.matchList.Add(_core.checkedCells[i]);
                            }
                            //Debug.Log($"{_core.matchList.Count} in list matchList");
                            _core.checkedCells.Clear();
                        }
                    }
                }
            }
        }
        private void CheckColumn()
        {
            for (var x = 1; x <= _core.gridSize; x++)
            {
                var _cells = _core.cells.FindAll(n => n.posX == x && n.posY <= _core.gridSize);
                for (var y = 0; y < _cells.Count - 1; y++)
                {
                    var next = y + 1;
                    if (_cells[y].itemInCell.spriteId == _cells[next].itemInCell.spriteId)
                    {
                        if(!_core.checkedCells.Contains(_cells[y])) _core.checkedCells.Add(_cells[y]);
                        if(!_core.checkedCells.Contains(_cells[next])) _core.checkedCells.Add(_cells[next]);
                    }
                    else
                    {
                        if(_core.checkedCells.Count < 3) _core.checkedCells.Clear();
                        else
                        {
                            for (var i = 0; i < _core.checkedCells.Count; i++)
                            {
                                _core.matchList.Add(_core.checkedCells[i]);
                            }
                            _core.checkedCells.Clear();
                        }
                    }
                }
            }
        }
    }
}