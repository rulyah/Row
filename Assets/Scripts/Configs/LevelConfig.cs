using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/LevelsConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int levelId;
        public int firstTaskSpriteId;
        public int firstTaskCount;
        public int secondTaskSpriteId;
        public int secondTaskCount;

        public List<int> spriteId = new List<int>();
        
    }
}