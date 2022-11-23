using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;

namespace States
{
    public class CheckGridState : State<Core>
    {
        public CheckGridState(Core core) : base(core) {}

        private List<Slot> _checkedSlots;
        
        public override void OnEnter()
        {
            Debug.Log("CheckGridState");
            _checkedSlots = new List<Slot>();
            CheckLine();
            CheckColumn();
            if (Model.matchList.Count < 3)
            {
                //ChangeState(new InputState(_core));
                ChangeState(new SwapBeckState(_core));
            }
            else
            {
                ChangeState(new CheckCornerSlotState(_core));
            }
        }
        private void CheckLine()
        {
            for (var y = 1; y <= GameConfig.gridSize; y++)
            {
                //Debug.Log($"chek {y} line");
                var _slots = _core.slots.FindAll(n => n.posY == y);
                for (var x = 0; x < _slots.Count - 1; x++)
                {
                    //Debug.Log($"Check {x+1} pos");
                    var next = x + 1;
                    //Debug.Log($"sprite 1 - {x+1} + {y} - {_cells[x].itemInCell.spriteId}");
                    //Debug.Log($"sprite next {next+1} + {y} - {_cells[next].itemInCell.spriteId}");

                    if (_slots[x].itemInSlot.spriteId == _slots[next].itemInSlot.spriteId)
                    {
                        if(!_checkedSlots.Contains(_slots[x])) _checkedSlots.Add(_slots[x]);
                        //Debug.Log($"sovpalo, {x+1} + {y}  ++++ {next+1} + {y}");
                        if(!_checkedSlots.Contains(_slots[next])) _checkedSlots.Add(_slots[next]);
                        //Debug.Log($"{_core.checkedCells.Count} in list checked");
                    }
                    else
                    {
                        //Debug.Log($"Ne sovpalo {_core.checkedCells.Count} in list");
                        if(_checkedSlots.Count < 3) _checkedSlots.Clear();
                        else
                        {
                            for (var i = 0; i < _checkedSlots.Count; i++)
                            {
                                Model.matchList.Add(_checkedSlots[i]);
                            }
                            //Debug.Log($"{_core.matchList.Count} in list matchList");
                            _checkedSlots.Clear();
                        }
                    }
                }
            }
        }
        private void CheckColumn()
        {
            for (var x = 1; x <= GameConfig.gridSize; x++)
            {
                var _slots = _core.slots.FindAll(n => n.posX == x && n.posY <= GameConfig.gridSize);
                for (var y = 0; y < _slots.Count - 1; y++)
                {
                    var next = y + 1;
                    if (_slots[y].itemInSlot.spriteId == _slots[next].itemInSlot.spriteId)
                    {
                        if(!_checkedSlots.Contains(_slots[y])) _checkedSlots.Add(_slots[y]);
                        if(!_checkedSlots.Contains(_slots[next])) _checkedSlots.Add(_slots[next]);
                    }
                    else
                    {
                        if(_checkedSlots.Count < 3) _checkedSlots.Clear();
                        else
                        {
                            for (var i = 0; i < _checkedSlots.Count; i++)
                            {
                                Model.matchList.Add(_checkedSlots[i]);
                            }
                            _checkedSlots.Clear();
                        }
                    }
                }
            }
        }
    }
}