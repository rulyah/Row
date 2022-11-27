using System.Collections.Generic;
using Configs;
using States;
using UI;
using UnityEngine;
using Screen = UI.Screen;

public class Model : MonoBehaviour
{
    public static List<Slot> matchList;
    
    public int movesCount;
    public static int score;
    //public int currentLevel { get; private set; } = 1;
    //public LevelConfig currentConfig;
    public static int firstGoalSpriteId { get; private set; }
    public static int secondGoalSpriteId { get; private set; }
    public static int firstGoalCount { get; private set; }
    public static int secondGoalCount { get; private set; }

    public void Init()
    {
        SetLevelTaskState.onSetTask += OnSetTask;
        matchList = new List<Slot>();
        RemoveMatchItemsState.onScoreChanged += OnScoreChanged;
        RemoveMatchItemsState.onFirstGoalAmountChanged += OnFirstGoalAmountChanged;
        RemoveMatchItemsState.onSecondGoalAmountChanged += OnSecondGoalAmountChanged;
        Screen.onGameRestart += Refresh;
        
    }

    private void OnFirstGoalAmountChanged()
    {
        firstGoalCount--;
        if (firstGoalCount <= 0)
        {
            firstGoalCount = 0;
            RemoveMatchItemsState.onFirstGoalAmountChanged -= OnFirstGoalAmountChanged;
        }
    }
    
    private void OnSecondGoalAmountChanged()
    {
        secondGoalCount--;
        if (secondGoalCount <= 0)
        {
            secondGoalCount = 0;
            RemoveMatchItemsState.onSecondGoalAmountChanged -= OnSecondGoalAmountChanged;
        }
    }

    private void OnScoreChanged(int value)
    {
        score += value;
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

    private void Refresh()
    {
        matchList.Clear();
        movesCount = 0;
        score = 0;
        Restart();
    }

    private void Restart()
    {
        SetLevelTaskState.onSetTask -= OnSetTask;
        RemoveMatchItemsState.onScoreChanged -= OnScoreChanged;
        RemoveMatchItemsState.onFirstGoalAmountChanged -= OnFirstGoalAmountChanged;
        RemoveMatchItemsState.onSecondGoalAmountChanged -= OnSecondGoalAmountChanged;
        Screen.onGameRestart -= Refresh;
    }
}