using System.Collections.Generic;
using Commands;

namespace States
{
    public class CheckGridState : State<Core>
    {
        public CheckGridState(Core core) : base(core) {}

        private List<Slot> _checkedSlots;
        
        public override void OnEnter()
        {
            _checkedSlots = new List<Slot>();
            if(Model.levelModel.matchList.Count > 0) Model.levelModel.matchList.Clear();
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
                else ChangeState(new InputState(_core));
            }
        }
    }
}