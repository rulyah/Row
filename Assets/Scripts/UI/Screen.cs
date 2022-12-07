using System;
using UnityEngine;

namespace UI
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public static event Action onGameRestart;
        public virtual void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        protected void RestartGame()
        {
            onGameRestart?.Invoke();
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}