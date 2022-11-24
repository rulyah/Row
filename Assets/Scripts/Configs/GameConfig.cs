namespace Configs
{
    public static class GameConfig
    {
        public const int gridSize = 8;
        public static Slot _firstSlot;
        public static Slot _secondSlot;
        public static LevelConfig _currentLevelConfig;
        public static bool isWrongMove = false;
        public static bool isNeedToCheckGrid = false;
        public static bool isInput = false;
    }
}