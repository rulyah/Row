using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/SpritesConfig", fileName = "SpritesConfig")]

    public class SpritesConfig : ScriptableObject
    {
        public List<Sprite> sprites;
    }
}