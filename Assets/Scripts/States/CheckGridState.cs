using System.Collections.Generic;
using Commands;
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
            if(Model.levelModel.matchList.Count > 0) Model.levelModel.matchList.Clear();
            //CheckLine();
            //CheckColumn();
            CheckGridCommand.CheckGrid(_core, _checkedSlots);
            if (Model.levelModel.matchList.Count >= 3)
            {
                if (Model.levelModel.isInput) Model.levelModel.isInput = false;
                ChangeState(new CheckCornerSlotState(_core));
            }
            else
            {
                if (Model.levelModel.isInput)
                {
                    Model.levelModel.isInput = false;
                    Model.levelModel.isWrongMove = true;
                    ChangeState(new SwapState(_core));
                }
                else
                {
                    ChangeState(new InputState(_core));
                }
            }
        }
        
        /*private void CheckLine()
        {
            for (var y = 1; y <= GameConfig.gridSize; y++)
            {
                var _slots = _core.slots.FindAll(n => n.posY == y);
                for (var x = 0; x < _slots.Count - 1; x++)
                {
                    var next = x + 1;

                    if (_slots[x].itemInSlot.spriteId == _slots[next].itemInSlot.spriteId)
                    {
                        if(!_checkedSlots.Contains(_slots[x])) _checkedSlots.Add(_slots[x]);
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
        }*/
    }
}