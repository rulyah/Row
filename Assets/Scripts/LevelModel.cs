using System.Collections.Generic;
using Configs;

public class LevelModel
{
    public List<Slot> matchList;
    public List<Item> hiddenItems;

    public int movesCount;
    public int score;
    public int firstGoalCount;
    public int secondGoalCount;
    
    public Slot firstSlot;
    public Slot secondSlot;
    public LevelConfig currentLevelConfig;
    public bool isWrongMove = false;
    public bool isNeedToCheckGrid = false;
    public bool isInput = false;
    public bool isVictory = false;

    public LevelModel()
    {
        matchList = new List<Slot>();
        hiddenItems = new List<Item>();
    }
}