using System.Collections.Generic;
using States;
using UnityEngine;

namespace UI
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private CompleteScreen _completeScreen;
        [SerializeField] private GameScreen _gameScreen;


        public void SetTask(List<Sprite> sprites)
        {
            _gameScreen.Refresh(sprites);
        }
    }
}