using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CompleteScreen : Screen
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _score;

        public static event Action onNextLevelClick;
        private void Init()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _nextButton.onClick.AddListener(NextLvl);
            _score.text = Model.levelModel.score.ToString();
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
            onNextLevelClick?.Invoke();
        }
    }
}