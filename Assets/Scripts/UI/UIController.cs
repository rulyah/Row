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
            RemoveMatchItemsState.onScoreChanged += OnScoreChanged;
            RemoveMatchItemsState.onFirstGoalAmountChanged += OnGoalCountChanged;
            RemoveMatchItemsState.onSecondGoalAmountChanged += OnGoalCountChanged;
            VictoryState.onVictory += OnVictory;
        }

        private void OnVictory()
        {
            _completeScreen.Show();
        }

        private void OnScoreChanged(int value)
        {
            _gameScreen.ScoreRefresh();
        }

        public void OnGoalCountChanged()
        {
            _gameScreen.GoalCountRefresh();
        }

        private void OnPauseClick()
        {
            Debug.Log("click");
            _pauseScreen.Show();
        }

        public void SetTask(List<Sprite> sprites)
        {
            _gameScreen.SetTask(sprites);
        }
    }
}