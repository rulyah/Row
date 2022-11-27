namespace Configs
{
    public static class GameConfig
    {
        public const int gridSize = 8;
        public static Slot firstSlot;
        public static Slot secondSlot;
        public static LevelConfig _currentLevelConfig;
        public static int currentLevel = 1;
        public static bool isWrongMove = false;
        public static bool isNeedToCheckGrid = false;
        public static bool isInput = false;
        public static bool isVictory = false;
    }
}