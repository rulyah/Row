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


        public void Init()
        {
            _gameScreen.Init();
            _gameScreen.onPauseButtonClick += OnPauseClick;
            RemoveMatchItemsState.onScoreChanged += OnUIChanged;
            RemoveMatchItemsState.onGoalAmountChanged += OnUIChanged;
        }

        private void OnUIChanged(int value)
        {
            Refresh();
        }

        private void OnPauseClick()
        {
            Debug.Log("click");
            _pauseScreen.Show();
        }

        public void Refresh()
        {
            _gameScreen.Refresh();
        }

        public void SetTask(List<Sprite> sprites)
        {
            _gameScreen.SetTask(sprites);
        }
    }
}