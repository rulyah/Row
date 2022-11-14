using System;
using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class MoveItemsState : State<Game>
    {
        public MoveItemsState(Game core) : base(core) { }

        public static event Action<int> onScoreChanged;
        public static event Action<int> onFirstFruitAmountChanged;
        public static event Action<int> onSecondFruitAmountChanged;

        public override void OnEnter()
        {
            Debug.Log("MoveCellsState");

            //Time.timeScale = 0.0f;
            _core.StartCoroutine(ChangMatchItemsScale(RemoveMatchListCells));
        }

        public override void OnExit()
        {
            onScoreChanged?.Invoke(_core.score);
        }

        private void RemoveMatchListCells()
        {
            for (var i = _core.matchList.Count - 1; i >= 0; i--)
            {
                CheckLvlTask(_core.matchList[i]);
                _core.matchList[i].DestroyItem();
                _core.score += 100;
                MoveItemDown(_core.matchList[i]);
            }
            /*foreach (var cell in _core.matchList)
            {
                //_core.cells.RemoveAt(_core.cells.IndexOf(_core.cells.Find(n => n.buttonId == cell.buttonId)));
                CheckLvlTask(cell);
                cell.DestroyItem();
                _core.score += 100;
                MoveItemDown(cell);
            }*/

            _core.PlayDelay(0.2f, () =>
            {
                _core.matchList.Clear();
                ChangeState(new HideItemState(_core));
            });
        }
        private void MoveItemDown(Cell cell)
        {
            Debug.Log($"pos cell {cell.posX} {cell.posY}");
            var column = _core.cells.FindAll(n => n.posX == cell.posX && n.posY > cell.posY);
            
            for (var i = 0; i < column.Count - 1; i++)
            {
                Debug.Log($"upper cell pos{column[i].posX} {column[i].posY}");
                if(i == 0) column[i].itemInCell.SetParent(cell);
                var next = i + 1;
                column[next].itemInCell.SetParent(column[i]);
                Debug.Log($"item pos{column[next].posX} {column[next].posY} change to {column[i].posX} {column[i].posY}");
            }
            /*foreach (var upperCell in _core.cells.FindAll(n => n.posX == cell.posX && n.posY > cell.posY))
            {
                upperCell.itemInCell.SetParent(_core.cells.Find(n => n.posX == upperCell.posX && n.posY == upperCell.posY - 1));
            }*/
            _core.CreateItem(cell.posX, _core.gridSize + 5);
        }

        private void CheckLvlTask(Cell cell)
        {
            if (cell.itemInCell.spriteId == _core.lvlConfig.firstTaskSpriteId)
            {
                _core.lvlConfig.firstTaskCount--;
                onFirstFruitAmountChanged?.Invoke(_core.lvlConfig.firstTaskCount);
            }
            if (cell.itemInCell.spriteId == _core.lvlConfig.secondTaskSpriteId)
            {
                _core.lvlConfig.secondTaskCount--;
                onSecondFruitAmountChanged?.Invoke(_core.lvlConfig.secondTaskCount);
            }
        }

        private IEnumerator ChangMatchItemsScale(Action action)
        {
            var scale = new Vector3(0.05f, 0.05f);
            while (_core.matchList[0].itemInCell.transform.localScale.x < 1.3f)
            {
                foreach (var cell in _core.matchList)
                {
                    cell.itemInCell.transform.localScale += scale;
                }
                yield return new WaitForSeconds(0.05f);
            }
            action?.Invoke();
        }
    }
}