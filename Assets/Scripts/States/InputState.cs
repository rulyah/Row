using System;
using Configs;
using UI;
using UnityEngine;

namespace States
{
    public class InputState : State<Core>
    {
        public InputState(Core core) : base(core) {}

        public static event Action onMoveCountChang;
        public override void OnEnter()
        {
            if(Model.levelModel.firstSlot != null) ResetSelection();
            foreach (var slot in _core.slots.FindAll(n => n.posX <= GameConfig.gridSize && n.posY <= GameConfig.gridSize))
            {
                slot.onButtonClick += OnButtonClick;
                slot.itemInSlot.onDownSwipe += OnVerticalSwipe;
                slot.itemInSlot.onUpSwipe += OnVerticalSwipe;
                slot.itemInSlot.onLeftSwipe += OnHorizontalSwipe;
                slot.itemInSlot.onRightSwipe += OnHorizontalSwipe;
            }
            GameScreen.onPauseButtonClick += OnPauseClick;
            if(Model.levelModel.isVictory) ChangeState(new VictoryState(_core));
        }

        private void OnVerticalSwipe(Slot slot, int nextPosY)
        {
            if (!CanSwap(nextPosY)) return;
            Model.levelModel.firstSlot = slot;
            Model.levelModel.secondSlot = _core.slots.Find(n => n.posX == Model.levelModel.firstSlot.posX && n.posY == nextPosY);
            Model.levelModel.movesCount++;
            ChangeState(new SwapState(_core));
        }
        
        private void OnHorizontalSwipe(Slot slot, int nextPosX)
        {
            if (!CanSwap(nextPosX)) return;
            Model.levelModel.firstSlot = slot; 
            Model.levelModel.secondSlot = _core.slots.Find(n => n.posY == Model.levelModel.firstSlot.posY && n.posX == nextPosX);
            Model.levelModel.movesCount++;
            ChangeState(new SwapState(_core));
        }

        private void OnPauseClick()
        {
            ChangeState(new PauseState(_core));
        }

        public override void OnExit()
        {
            foreach (var slot in _core.slots.FindAll(n => n.posX <= GameConfig.gridSize && n.posY <= GameConfig.gridSize))
            {
                slot.onButtonClick -= OnButtonClick;
                slot.itemInSlot.onDownSwipe -= OnVerticalSwipe;
                slot.itemInSlot.onUpSwipe -= OnVerticalSwipe;
                slot.itemInSlot.onLeftSwipe -= OnHorizontalSwipe;
                slot.itemInSlot.onRightSwipe -= OnHorizontalSwipe;
            }
            GameScreen.onPauseButtonClick += OnPauseClick;
            Model.levelModel.isInput = true;
            onMoveCountChang?.Invoke();
        }

        private void OnButtonClick(Slot slot)
        {
            if (Model.levelModel.firstSlot == null)
            {
                Model.levelModel.firstSlot = slot;
                Model.levelModel.firstSlot.itemInSlot.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                Model.levelModel.secondSlot = slot;
                Model.levelModel.firstSlot.itemInSlot.transform.localScale = Vector3.one;
                if (Model.levelModel.firstSlot != Model.levelModel.secondSlot)
                {
                    var distance = Vector3.Distance(Model.levelModel.firstSlot.transform.position, 
                        Model.levelModel.secondSlot.transform.position);
                    if (distance < 0.65f)
                    {
                        Model.levelModel.movesCount++;
                        ChangeState(new SwapState(_core));
                    }
                    else ResetSelection();
                }
                else
                {
                    AudioController.instance.Play(GameConfig.soundsConfig.wrongMoveSound);
                    ResetSelection();
                }
            }
        }
        
        private void ResetSelection()
        {
            Model.levelModel.firstSlot = null;
            Model.levelModel.secondSlot = null;
        }

        private bool CanSwap(int nextPos)
        {
            return nextPos is > 1 and < 8;
        }
    }
}