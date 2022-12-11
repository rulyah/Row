using System.Collections.Generic;
using Configs;

public static class Model
{
    public static int currentLevel = 1;


    public static LevelModel levelModel;
    //public static int movesCount;
    //public static int score;
    //public static int firstGoalSpriteId;
    //public static int secondGoalSpriteId;
    
    
    /*public void Init()
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
    }*/
}