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
        [SerializeField] private Slider _slider;
        [SerializeField] private List<Star> _stars;

        public static event Action onPauseButtonClick;

        private int _currentStarsCount;
        private int _firstGoalCount;
        private int _secondGoalCount;
        private float _currentSliderValue;
        
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
            _secondGoalImage.sprite = sprites[Model.secondGoalSpriteId];
            _firstGoalText.text = Model.firstGoalCount.ToString();
            _secondGoalText.text = Model.secondGoalCount.ToString();
            _firstGoalCount = Model.firstGoalCount;
            _secondGoalCount = Model.secondGoalCount;
        }

        public void ScoreRefresh()
        {
            _score.text = Model.score.ToString();
        }

        public void MoveRefresh()
        {
            _moves.text = Model.movesCount.ToString();
        }

        public void GoalCountRefresh()
        {
            _firstGoalText.text = Model.firstGoalCount.ToString();
            _secondGoalText.text = Model.secondGoalCount.ToString();
            _slider.value = 1 - (float)(Model.firstGoalCount + Model.secondGoalCount) / (_firstGoalCount + _secondGoalCount);
            if(_slider.value > 0.25f) ShowStar(_stars[0]);
            if(_slider.value > 0.6f) ShowStar(_stars[1]);
            if(_slider.value > 0.99f) ShowStar(_stars[2]);
        }

        private void ShowStar(Star star)
        {
            if (star.isVisible == true) return;
            star.ShowStar();
            _currentStarsCount++;
        }
    }
}