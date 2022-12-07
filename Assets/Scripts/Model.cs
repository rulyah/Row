using System.Collections.Generic;
using Configs;
using States;
using UnityEngine;
using Screen = UI.Screen;

public class Model : MonoBehaviour
{
    public static List<Slot> matchList;
    
    public static int movesCount;
    public static int score;
    public static int firstGoalSpriteId;
    public static int secondGoalSpriteId;
    public static int firstGoalCount;
    public static int secondGoalCount;

    public void Init()
    {
        SetLevelTaskState.onSetTask += OnSetTask;
        matchList = new List<Slot>();
        Screen.onGameRestart += Restart;
    }

    private void OnSetTask(LevelConfig config)
    {
        firstGoalSpriteId = config.firstTaskSpriteId;
        secondGoalSpriteId = config.secondTaskSpriteId;
        firstGoalCount = config.firstTaskCount;
        secondGoalCount = config.secondTaskCount;
        GameConfig.currentLevel = config.levelId;
        GameConfig._currentLevelConfig = config;
    }

    private void Restart()
    {
        Debug.Log("is refresh");
        //matchList.Clear();
        movesCount = 0;
        score = 0;
    }
}