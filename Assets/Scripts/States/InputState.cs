using UnityEngine;

namespace States
{
    public class InputState : State<Core>
    {
        public InputState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("InputState");
            if(_core.model.firstSlot != null) ResetSelection();
            foreach (var slot in _core.slots)
            {
                slot.onButtonClick += OnButtonClick;
                slot.itemInSlot.onDownSwipe += OnVerticalSwipe;
                slot.itemInSlot.onUpSwipe += OnVerticalSwipe;
                slot.itemInSlot.onLeftSwipe += OnHorizontalSwipe;
                slot.itemInSlot.onRightSwipe += OnHorizontalSwipe;
            }
        }

        private void OnVerticalSwipe(Slot slot, int nextPosY)
        {
            if (!CanSwap(nextPosY)) return;
            _core.model.firstSlot = slot;
            _core.model.secondSlot = _core.slots.Find(n => n.posX == _core.model.firstSlot.posX && n.posY == nextPosY);
            ChangeState(new SwapState(_core));
        }
        private void OnHorizontalSwipe(Slot slot, int nextPosX)
        {
            if (!CanSwap(nextPosX)) return;
            _core.model.firstSlot = slot; 
            _core.model.secondSlot = _core.slots.Find(n => n.posY == _core.model.firstSlot.posY && n.posX == nextPosX);
            ChangeState(new SwapState(_core));
        }

        public override void OnExit()
        {
            foreach (var slot in _core.slots)
            {
                slot.onButtonClick -= OnButtonClick;
                slot.itemInSlot.onDownSwipe -= OnVerticalSwipe;
                slot.itemInSlot.onUpSwipe -= OnVerticalSwipe;
                slot.itemInSlot.onLeftSwipe -= OnHorizontalSwipe;
                slot.itemInSlot.onRightSwipe -= OnHorizontalSwipe;
            }
        }

        private void OnButtonClick(Slot slot)
        {
            if (_core.model.firstSlot == null)
            {
                _core.model.firstSlot = slot;
                _core.model.firstSlot.itemInSlot.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                _core.model.secondSlot = slot;
                _core.model.firstSlot.itemInSlot.transform.localScale = Vector3.one;
                if (_core.model.firstSlot != _core.model.secondSlot)
                {
                    var distance = Vector3.Distance(_core.model.firstSlot.transform.position, _core.model.secondSlot.transform.position);
                    if (distance < 0.65f)
                    {
                        ChangeState(new SwapState(_core));
                    }
                    else
                    {
                        ResetSelection();
                    }
                }
                else
                {
                    ResetSelection();
                }
            }
        }
        
        private void ResetSelection()
        {
            _core.model.firstSlot = null;
            _core.model.secondSlot = null;
        }

        private bool CanSwap(int nextPos)
        {
            return nextPos is > 1 and < 8;
        }
    }
}