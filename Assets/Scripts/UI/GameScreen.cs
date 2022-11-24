using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moves;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Image _firstGoalImage;
        [SerializeField] private Image _secondGoalImage;
        [SerializeField] private TextMeshProUGUI _firstGoalText;
        [SerializeField] private TextMeshProUGUI _secondGoalText;

        public event Action onPauseButtonClick;
        
        public void Init()
        {
            _pauseButton.onClick.AddListener(PauseClick);
        }

        private void PauseClick()
        {
            onPauseButtonClick?.Invoke();
        }

        public void SetTask(List<Sprite> sprites)
        {
            _firstGoalImage.sprite = sprites[Model.firstGoalSpriteId];
            _secondGoalImage.sprite = sprites[Model.secondGoalCount];
            _firstGoalText.text = Model.firstGoalCount.ToString();
            _secondGoalText.text = Model.secondGoalCount.ToString();
        }

        public void Refresh()
        {
            _firstGoalText.text = Model.firstGoalCount.ToString();
            _secondGoalText.text = Model.secondGoalCount.ToString();
            _score.text = Model.score.ToString();
        }

        private void OnFirstFruitAmountChanged(int amount)
        {
            _firstGoalText.text = amount.ToString();
        }

        private void OnSecondFruitAmountChanged(int amount)
        {
            _secondGoalText.text = amount.ToString();
        }


        private void OnScoreChanged(int scoreCount)
        {
            _score.text = scoreCount.ToString();
        }

        private void OnCellsMove(int moveCount)
        {
            _moves.text = moveCount.ToString();
        }

    }
}