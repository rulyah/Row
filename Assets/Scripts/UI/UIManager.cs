using System;
using StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moves;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private PauseScreen _pause;
        public Image _firstFruitImage;
        public Image _secondFruitImage;
        public TextMeshProUGUI _firstFruitText;
        public TextMeshProUGUI _secondFruitText;
        
        public void Init()
        {
            SwapState.onCellsMove += OnCellsMove;
            MoveCellsState.onScoreChanged += OnScoreChanged;
            MoveCellsState.onFirstFruitAmountChanged += OnFirstFruitAmountChanged;
            MoveCellsState.onSecondFruitAmountChanged += OnSecondFruitAmountChanged;
            _pauseButton.onClick.AddListener(PauseClick);
        }

        private void OnFirstFruitAmountChanged(int amount)
        {
            _firstFruitText.text = amount.ToString();
        }

        private void OnSecondFruitAmountChanged(int amount)
        {
            _secondFruitText.text = amount.ToString();
        }


        private void OnScoreChanged(int scoreCount)
        {
            _score.text = scoreCount.ToString();
        }

        private void OnCellsMove(int moveCount)
        {
            _moves.text = moveCount.ToString();
        }

        private void PauseClick()
        {
            _pause.Show();
        }
    }
}