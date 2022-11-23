using System.Collections.Generic;
using Configs;
using States;
using UnityEngine;

public class Model : MonoBehaviour
{
    public static List<Slot> matchList;
    //public Stack<Slot> matchSlots;
    
    public Slot firstSlot;
    public Slot secondSlot;
    
    public int movesCount;
    public int score;
    public int currentLevel { get; private set; }
    public LevelConfig currentConfig;
    public static int firstGoalSpriteId { get; private set; }
    public static int secondGoalSpriteId { get; private set; }
    public static int firstGoalCount { get; private set; }
    public static int secondGoalCount { get; private set; }

    public void Init()
    {
        SetLevelTaskState.onSetTask += OnSetTask;
        matchList = new List<Slot>();
        //matchSlots = new Stack<Slot>();
    }

    private void OnSetTask(LevelConfig config)
    {
        firstGoalSpriteId = config.firstTaskSpriteId;
        secondGoalSpriteId = config.secondTaskSpriteId;
        firstGoalCount = config.firstTaskCount;
        secondGoalCount = config.secondTaskCount;
        currentLevel = config.levelId;
        currentConfig = config;
    }
}