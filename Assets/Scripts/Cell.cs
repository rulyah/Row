using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour//, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Button _button;
    public Item itemInCell;
    public int buttonId;
    //public int spriteId;
    //public Image image;

    public int posX;
    public int posY;

    //private Vector3 _movePoint;
    //private Vector3 _currentPos;
    //private Vector3 _startDragPoint;
    //private Vector3 _finishDragPoint;

    //private float _tick;
    //private float _duration = 0.2f;
    //private bool _isMove;
    
    public event Action<int> onButtonClick;
    //public event Action<int, int> onLeftSwipe;
    //public event Action<int, int> onRightSwipe;
    //public event Action<int, int> onUpSwipe;
    //public event Action<int, int> onDownSwipe;

    public void RefreshItem()
    {
        itemInCell = GetComponentInChildren<Item>();
    }

    public void DestroyItem()
    {
        Destroy(itemInCell.gameObject);
        itemInCell = null;
    }

    public void Init()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        onButtonClick?.Invoke(buttonId);
    }

    /*public void Move(Vector3 movePoint)
    {
        if(_isMove) return;
        _movePoint = movePoint;
        _currentPos = transform.localPosition;
        _isMove = true;
        RefreshPos();
    }

    private void RefreshPos()
    {
        if (_movePoint.x > _currentPos.x) posX++;
        if (_movePoint.x < _currentPos.x) posX--;
        if (_movePoint.y < _currentPos.y) posY--;
        if (_movePoint.y > _currentPos.y) posY++;
    }
    public void Hide()
    {
        spriteId = 10;
        image.color = new Color(255,255,255,0);
        _button.interactable = false;
    }

    public void Show()
    {
        spriteId = buttonId % 7;
        image.color = new Color(255,255,255,100);
        _button.interactable = true;
    }


    private void Update()
    {
        if (_isMove)
        {
            _tick += Time.deltaTime;
            var progress = Mathf.Clamp01(_tick / _duration);
            transform.localPosition = Vector3.Lerp(_currentPos, _movePoint, progress);
            if (progress >= 0.99f)
            {
                _currentPos = _movePoint;
                _tick = 0.0f;
                _isMove = false;
            }
        }   
    }

    public int GetRandomSpriteId()
    {
        return Random.Range(0, 7);
    }
    
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPoint = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _finishDragPoint = Input.mousePosition;
        if (_startDragPoint == _finishDragPoint) return;

        var direction = Vector3.Normalize(_finishDragPoint - _startDragPoint);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0.0f) onRightSwipe?.Invoke(buttonId, posX + 1);
            else onLeftSwipe?.Invoke(buttonId, posX - 1);
        }
        else if (direction.y > 0.0f) onUpSwipe?.Invoke(buttonId, posY + 1);
        else onDownSwipe?.Invoke(buttonId, posY - 1);
    }

    public void OnDrag(PointerEventData eventData){}*/
}