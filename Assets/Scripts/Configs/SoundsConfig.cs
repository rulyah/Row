using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/SoundsConfigs", fileName = "SoundConfig")]
    public class SoundsConfig : ScriptableObject
    {
        public AudioClip swapSound;
        public AudioClip wrongMoveSound;
        public AudioClip burningSound;
        public AudioClip victorySound;
    }
}