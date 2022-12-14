using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreen : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void Init()
        {
            _closeButton.onClick.AddListener(Hide);
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
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

        protected override void Hide()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _restartButton.onClick.RemoveListener(RestartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
            base.Hide();
        }
    }
}