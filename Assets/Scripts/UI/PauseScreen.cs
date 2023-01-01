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
        [SerializeField] private Button _songButton;
        [SerializeField] private Button _soundButton;

        private bool _isSongPlay = true;
        private bool _isSoundsPlay = true;
        private RectTransform _songButtonRectTransform;
        private RectTransform _soundButtonRectTransform;
        
        public static event Action onCloseScreenClick;
        public static event Action<bool> onSongVolumeChange;
        public static event Action<bool> onSoundVolumeChange;
        
        private void Init()
        {
            _closeButton.onClick.AddListener(OnCloseClick);
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _songButton.onClick.AddListener(TurnSong);
            _soundButton.onClick.AddListener(TurnSound);
            _songButtonRectTransform = _songButton.GetComponent<RectTransform>();
            _soundButtonRectTransform = _soundButton.GetComponent<RectTransform>();
        }

        private void OnCloseClick()
        {
            onCloseScreenClick?.Invoke();
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }

        private void TurnSong()
        {
            _songButtonRectTransform.anchoredPosition = new Vector2(_isSongPlay ? 210.0f : 0.0f, 0.0f);
            _isSongPlay = !_isSongPlay;
            onSongVolumeChange?.Invoke(_isSongPlay);
        }

        private void TurnSound()
        {
            _soundButtonRectTransform.anchoredPosition = new Vector2(_isSoundsPlay ? 210.0f : 0.0f, 0.0f);
            _isSoundsPlay = !_isSoundsPlay;
            onSoundVolumeChange?.Invoke(_isSoundsPlay);
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
            _songButton.onClick.RemoveListener(TurnSong);
            _soundButton.onClick.RemoveListener(TurnSound);
            base.Hide();
        }
    }
}