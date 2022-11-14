using UnityEngine;

namespace StateMachine
{
    public class InputState : State<Game>
    {
        public InputState(Game core) : base(core) { }

        public override void OnEnter()
        {
            Debug.Log("InputState");
            if(_core.firstCell != null) ResetSelection();
            foreach (var cell in _core.cells)
            {
                cell.onButtonClick += OnButtonClick;
                cell.itemInCell.onDownSwipe += OnVerticalSwipe;
                cell.itemInCell.onUpSwipe += OnVerticalSwipe;
                cell.itemInCell.onLeftSwipe += OnHorizontalSwipe;
                cell.itemInCell.onRightSwipe += OnHorizontalSwipe;
            }
        }

        private void OnVerticalSwipe(int buttonId, int nextPos)
        {
            if (!CanSwap(nextPos)) return;
            _core.firstCell = FindCellByButtonId(buttonId);
            _core.secondCell = _core.cells.Find(n => n.posX == _core.firstCell.posX && n.posY == nextPos);
            ChangeState(new SwapState(_core));
        }
        private void OnHorizontalSwipe(int buttonId, int nextPos)
        {
            if (!CanSwap(nextPos)) return;
            _core.firstCell = FindCellByButtonId(buttonId);
            _core.secondCell = _core.cells.Find(n => n.posY == _core.firstCell.posY && n.posX == nextPos);
            ChangeState(new SwapState(_core));
        }

        public override void OnExit()
        {
            foreach (var cell in _core.cells)
            {
                cell.onButtonClick -= OnButtonClick;
                cell.itemInCell.onDownSwipe -= OnVerticalSwipe;
                cell.itemInCell.onUpSwipe -= OnVerticalSwipe;
                cell.itemInCell.onLeftSwipe -= OnHorizontalSwipe;
                cell.itemInCell.onRightSwipe -= OnHorizontalSwipe;
            }
        }

        private void OnButtonClick(int buttonId)
        {
            if (_core.firstCell == null)
            {
                _core.firstCell = FindCellByButtonId(buttonId);
                _core.firstCell.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                _core.secondCell = FindCellByButtonId(buttonId);
                _core.firstCell.transform.localScale = Vector3.one;
                if (_core.firstCell != _core.secondCell)
                {
                    var distance = Vector3.Distance(_core.firstCell.transform.position, _core.secondCell.transform.position);
                    if (distance < 0.65f)
                    {
                        ChangeState(new SwapState(_core));
                    }
                }
                else
                {
                    ResetSelection();
                }
            }
        }
        private Cell FindCellByButtonId(int buttonId)
        {
            return _core.cells.Find(n => n.buttonId == buttonId);
        }
        
        private void ResetSelection()
        {
            _core.firstCell = null;
            _core.secondCell = null;
        }

        private bool CanSwap(int nextPos)
        {
            return nextPos is > 1 and < 8;
        }
    }
}