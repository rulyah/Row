using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using StateMachine;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private Cell _cell;
    [SerializeField] private Item _item;
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
        var cell = Instantiate(_cell, _transform);
        cell.transform.localPosition = GridPosToLocal(posX, posY);
        cell.posX = posX;
        cell.posY = posY;
        cells.Add(cell);
        cell.buttonId = buttonsCount;
        buttonsCount++;
        cell.Init();
        CreateItem(posX, posY);
    }

    public void CreateItem(int posX, int posY)
    {
        var cell = cells.Find(n => n.posX == posX && n.posY == posY);
        cell.itemInCell = Instantiate(_item, cell.transform);
        cell.itemInCell.spriteId = buttonsCount < 104 ? lvlConfig.spriteId[buttonsCount] : cell.itemInCell.GetRandomSpriteId();
        cell.itemInCell.image.sprite = sprites[cell.itemInCell.spriteId];
        cell.itemInCell.SetParent(cell);
    }

    public Vector3 GridPosToLocal(int posX, int posY)
    {
        return new Vector3(posX * width / gridSize - width / 2 - cellSizeX / 2,
            posY * height / gridSize - height / 2 - cellSizeY / 2);
    }

    public void HideItem(int posX, int posY)
    {
        var cell = cells.Find(n => n.posX == posX && n.posY == posY);
        if (cell.itemInCell.spriteId != 10)
        {
            cell.itemInCell.Hide();
        }
    }

        
        
    public void ItemSwap(Cell first, Cell second)
    {
        first.itemInCell.SetParent(second);
        second.itemInCell.SetParent(first);
        first.itemInCell.Move(Vector3.zero);
        //SetAsLastSibling
        second.itemInCell.Move(Vector3.zero);
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
            var f = cells.Find(n => n.posX == 2 && n.posY == 2);
            var s = cells.Find(n => n.posX == 2 && n.posY == 3);
            f.itemInCell.transform.parent = s.transform;
            Debug.Log("cell 2 2 item parent 2 3");
            f.itemInCell.Move(Vector3.zero);
        }
    }
}