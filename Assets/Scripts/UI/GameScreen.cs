using System;
using System.Collections.Generic;
using Configs;
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

        public void Init()
        {
            _pauseButton.onClick.AddListener(PauseClick);
        }

        private void PauseClick()
        {
            onPauseButtonClick?.Invoke();
        }

        public void Restart()
        {
            for (var i = 0; i < _stars.Count; i++)
            {
                if(_stars[i].isVisible == true) _stars[i].HideStar();
            }
            _slider.value = 0;
            _score.text = "0";
            _moves.text = "0";
        }

        public void SetTask()
        {
            _firstGoalImage.sprite = GameConfig.spritesConfig.sprites[Model.levelModel.currentLevelConfig.firstTaskSpriteId];
            _secondGoalImage.sprite = GameConfig.spritesConfig.sprites[Model.levelModel.currentLevelConfig.secondTaskSpriteId];
            _firstGoalText.text = Model.levelModel.firstGoalCount.ToString();
            _secondGoalText.text = Model.levelModel.secondGoalCount.ToString();
        }

        public void ScoreRefresh()
        {
            _score.text = Model.levelModel.score.ToString();
        }

        public void MoveRefresh()
        {
            _moves.text = Model.levelModel.movesCount.ToString();
        }

        public void GoalCountRefresh()
        {
            _firstGoalText.text = Model.levelModel.firstGoalCount.ToString();
            _secondGoalText.text = Model.levelModel.secondGoalCount.ToString();
            _slider.value = 1 - (float)(Model.levelModel.firstGoalCount + Model.levelModel.secondGoalCount)
                / (Model.levelModel.currentLevelConfig.firstTaskCount + Model.levelModel.currentLevelConfig.secondTaskCount);
            if(_slider.value > 0.25f) ShowStar(_stars[0]);
            if(_slider.value > 0.55f) ShowStar(_stars[1]);
            if(_slider.value > 0.99f) ShowStar(_stars[2]);
        }

        private void ShowStar(Star star)
        {
            if (star.isVisible == true) return;
            star.ShowStar();
        }
    }
}