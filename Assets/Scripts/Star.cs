using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    public bool isVisible = false;

    public void ShowStar()
    {
        _canvasGroup.alpha = 1;
        isVisible = true;
    }

    public void HideStar()
    {
        _canvasGroup.alpha = 0;
        isVisible = false;
    }
}