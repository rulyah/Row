using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using UI;
using UnityEngine;

namespace StateMachine
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Cell _cell;
        [SerializeField] private UIManager _ui;

        private StateMachine<Game> _stateMachine;
        public LevelsConfig lvlConfig { get; private set; }


        public List<Cell> cells = new List<Cell>();
        public List<Sprite> sprites = new List<Sprite>();
        public List<Cell> checkedCells;
        public List<Cell> matchList;

        public int buttonsCount;
        public int gridSize = 8;
        public Cell firstCell;
        public Cell secondCell;
        public int movesCount;
        public int score;

        //public int firstFruitSpriteId;
        //public int secondFruitSpriteId;
        //public int firstFruitAmount;
        //public int secondFruitAmount;



        private float cellSizeX;
        private float cellSizeY;
        private float width;
        private float height;


        private void Start()
        {
            width = _transform.sizeDelta.x;
            height = _transform.sizeDelta.y;
            cellSizeX = width / gridSize;
            cellSizeY = height / gridSize;
            _ui.Init();
            lvlConfig = Resources.Load<LevelsConfig>("LevelsConfig/1LevelConfig");
            SetLvlTask();
            _stateMachine = new StateMachine<Game>(new CreateBorderState(this));
        }

        private void SetLvlTask()
        {
            _ui._firstFruitImage.sprite = sprites[lvlConfig.firstTaskSpriteId];
            _ui._secondFruitImage.sprite = sprites[lvlConfig.secondTaskSpriteId];
            _ui._firstFruitText.text = lvlConfig.firstTaskCount.ToString();
            _ui._secondFruitText.text = lvlConfig.secondTaskCount.ToString();
        }

        public void CreateCell(int posX, int posY)
        {
            var cell = Instantiate(_cell, transform.parent = _transform);
            cell.transform.localPosition = GridPosToLocal(posX, posY);
            cell.posX = posX;
            cell.posY = posY;
            cells.Add(cell);
            cell.buttonId = buttonsCount;
            cell.spriteId = buttonsCount < 104 ? lvlConfig.spriteId[buttonsCount] : cell.GetRandomSpriteId();
            buttonsCount++;
            cell.image.sprite = sprites[cell.spriteId];
            cell.Init();
        }

        public Vector3 GridPosToLocal(int posX, int posY)
        {
            return new Vector3(posX * width / gridSize - width / 2 - cellSizeX / 2,
                posY * height / gridSize - height / 2 - cellSizeY / 2);
        }

        public void HideCell(int posX, int posY)
        {
            var cell = cells.Find(n => n.posX == posX && n.posY == posY);
            if (cell.spriteId != 10)
            {
                cell.Hide();
            }
        }

        public void CellDestroy(Cell cell)
        {
            Destroy(cell.gameObject);
        }
        
        public void CellSwap(Cell first, Cell second)
        {
            first.Move(new Vector3(second.transform.localPosition.x, second.transform.localPosition.y));
            //SetAsLastSibling
            second.Move(new Vector3(first.transform.localPosition.x, first.transform.localPosition.y));
        }

        public void PlayDelay(float waitTime, Action action)
        {
            StartCoroutine(Delay(waitTime,action));
        }
        private IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
            }
        }
    }
}