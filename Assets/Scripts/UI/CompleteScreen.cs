using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CompleteScreen : Screen
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextButton;

        private void Init()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _nextButton.onClick.AddListener(NextLvl);
        }

        public override void Show()
        {
            base.Show();
            Init();
        }

        public override void Hide()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _nextButton.onClick.RemoveListener(NextLvl);
            base.Hide();
        }

        public void NextLvl()
        {
            
        }
    }
}