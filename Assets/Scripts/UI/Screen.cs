using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void RestartGame()
        {
            onGameRestart?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}