using System;
using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class MoveCellsState : State<Game>
    {
        public MoveCellsState(Game core) : base(core) { }

        public static event Action<int> onScoreChanged;
        public static event Action<int> onFirstFruitAmountChanged;
        public static event Action<int> onSecondFruitAmountChanged;

        public override void OnEnter()
        {
            Debug.Log("MoveCellsState");

            //Time.timeScale = 0.0f;
            _core.StartCoroutine(ChangScale(RemoveCell));
        }

        public override void OnExit()
        {
            onScoreChanged?.Invoke(_core.score);
        }

        private void RemoveCell()
        {
            foreach (var cell in _core.matchList)
            {
                _core.cells.RemoveAt(_core.cells.IndexOf(_core.cells.Find(n => n.buttonId == cell.buttonId)));
                CheckLvlTask(cell);
                _core.CellDestroy(cell);
                _core.score += 100;
                MoveCellDown(cell);
            }

            _core.PlayDelay(0.2f, () =>
            {
                _core.matchList.Clear();
                ChangeState(new HideCornerCellState(_core));
            });
        }
        private void MoveCellDown(Cell cell)
        {
            foreach (var upperCell in _core.cells.FindAll(n => n.posX == cell.posX && n.posY > cell.posY))
            {
                upperCell.Move(_core.GridPosToLocal(upperCell.posX, upperCell.posY - 1));
            }
            _core.CreateCell(cell.posX, _core.gridSize + 5);
        }

        private void CheckLvlTask(Cell cell)
        {
            if (cell.spriteId == _core.lvlConfig.firstTaskSpriteId)
            {
                _core.lvlConfig.firstTaskCount--;
                onFirstFruitAmountChanged?.Invoke(_core.lvlConfig.firstTaskCount);
            }
            if (cell.spriteId == _core.lvlConfig.secondTaskSpriteId)
            {
                _core.lvlConfig.secondTaskCount--;
                onSecondFruitAmountChanged?.Invoke(_core.lvlConfig.secondTaskCount);
            }
        }

        private IEnumerator ChangScale(Action action)
        {
            var scale = new Vector3(0.05f, 0.05f);
            foreach (var cell in _core.matchList)
            {
                cell.transform.localScale += scale;
            }
            yield return new WaitForSeconds(0.05f);
            Debug.Log(_core.matchList[0].buttonId.ToString());
            if (_core.matchList[0] != null)
            {
                if(_core.matchList[0].transform.localScale.x < 1.3f) _core.StartCoroutine(ChangScale(action));
                else
                {
                    action?.Invoke();
                    _core.StopCoroutine(ChangScale(action));
                }
            }
            else
            {
                action?.Invoke();
                _core.StopCoroutine(ChangScale(action));
            }
        }
    }
}