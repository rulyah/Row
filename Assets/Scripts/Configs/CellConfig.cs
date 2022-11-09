using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    public class CellConfig
    {
        public List<Sprites> sprites;
        public int spriteId;

        public CellConfig()
        {
            sprites = new List<Sprites>();
            spriteId = 0;
        }
    }
}