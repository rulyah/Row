using Configs;
using UnityEngine;

namespace States
{
    public class InputState : State<Core>
    {
        public InputState(Core core) : base(core) {}

        public override void OnEnter()
        {
            if(GameConfig.isVictory) ChangeState(new VictoryState(_core));
            Debug.Log("InputState");
            if(GameConfig.firstSlot != null) ResetSelection();
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
            GameConfig.firstSlot = slot;
            GameConfig.secondSlot = _core.slots.Find(n => n.posX == GameConfig.firstSlot.posX && n.posY == nextPosY);
            ChangeState(new SwapState(_core));
        }
        private void OnHorizontalSwipe(Slot slot, int nextPosX)
        {
            if (!CanSwap(nextPosX)) return;
            GameConfig.firstSlot = slot; 
            GameConfig.secondSlot = _core.slots.Find(n => n.posY == GameConfig.firstSlot.posY && n.posX == nextPosX);
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
            GameConfig.isInput = true;
        }

        private void OnButtonClick(Slot slot)
        {
            if (GameConfig.firstSlot == null)
            {
                GameConfig.firstSlot = slot;
                GameConfig.firstSlot.itemInSlot.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                GameConfig.secondSlot = slot;
                GameConfig.firstSlot.itemInSlot.transform.localScale = Vector3.one;
                if (GameConfig.firstSlot != GameConfig.secondSlot)
                {
                    var distance = Vector3.Distance(GameConfig.firstSlot.transform.position, GameConfig.secondSlot.transform.position);
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
            GameConfig.firstSlot = null;
            GameConfig.secondSlot = null;
        }

        private bool CanSwap(int nextPos)
        {
            return nextPos is > 1 and < 8;
        }
    }
}