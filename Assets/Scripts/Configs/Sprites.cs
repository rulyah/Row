using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/Sprites", fileName = "Sprites")]
    public class Sprites : ScriptableObject
    {
        public List<Sprite> sprite = new List<Sprite>();
    }
}