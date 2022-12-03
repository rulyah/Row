using System.Collections.Generic;
using States;
using UnityEngine;

namespace UI
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] private PauseScreen _pauseScreen;
        public PauseScreen pauseScreen => _pauseScreen;
        [SerializeField] private CompleteScreen _completeScreen;
        public CompleteScreen completeScreen => _completeScreen;
        [SerializeField] private GameScreen _gameScreen;
        public Stack<Screen> _screens;

        public void Init()
        {
            _gameScreen.Init();
            _screens = new Stack<Screen>();
            InputState.onMoveCountChang += OnMoveCountChang;
            RemoveMatchItemsState.onScoreChanged += OnScoreChanged;
            RemoveMatchItemsState.onGoalAmountChanged += OnGoalAmountChanged;
        }

        private void OnMoveCountChang()
        {
            _gameScreen.MoveRefresh();
        }

        public void ShowScreen(Screen screen)
        {
            screen.Show();
            _screens.Push(screen);
        }

        public void CloseLastScreen()
        {
            _screens.Pop().Hide();
        }

        private void OnScoreChanged(int value)
        {
            _gameScreen.ScoreRefresh();
        }

        public void OnGoalAmountChanged()
        {
            _gameScreen.GoalCountRefresh();
        }

        public void SetTask(List<Sprite> sprites)
        {
            _gameScreen.SetTask(sprites);
        }
    }
}