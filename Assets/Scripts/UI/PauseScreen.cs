using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreen : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public static event Action onCloseScreenClick;
        private void Init()
        {
            _closeButton.onClick.AddListener(OnCloseClick);
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnCloseClick()
        {
            onCloseScreenClick?.Invoke();
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }

        public override void Show()
        {
            base.Show();
            Init();
        }

        public override void Hide()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _restartButton.onClick.RemoveListener(RestartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
            base.Hide();
        }
    }
}