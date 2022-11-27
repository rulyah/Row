using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _rectTransform;
    
    public Image image;
    public int spriteId;
    public Slot _parent;
    public bool isHide = false;


    private int _previousSpriteId;
    private Vector3 _startDragPoint;
    private Vector3 _finishDragPoint;
    private Vector3 _currentPos;
    private Vector3 _movePoint;
    private float _ticks;
    private float _duration = 0.2f;
    private bool _isMoving;


    public event Action<Slot, int> onUpSwipe;
    public event Action<Slot, int> onDownSwipe;
    public event Action<Slot, int> onLeftSwipe;
    public event Action<Slot, int> onRightSwipe;

    public void SetParent(Slot slot)
    {
        _parent = slot;
        transform.SetParent(slot.transform);
        _parent.SetItem(this);
    }

    public void Move(Slot slot)
    {
        if(_isMoving) return;
        SetParent(slot);
        _currentPos = transform.localPosition;
        _isMoving = true;
    }
    

    public void HideItem()
    {
        _parent = null;
        _previousSpriteId = spriteId;
        image.color = new Color(255, 255, 255, 0);
        spriteId = 10;
        isHide = true;
    }

    public void Show()
    {
        spriteId = _previousSpriteId;
        image.color = new Color(255, 255, 255, 255);
        isHide = false;
    }

    public void SetRandomSpriteId()
    {
        spriteId = Random.Range(0, 7);
        _previousSpriteId = spriteId;
    }

    public void OnBeginDrag(PointerEventData eventData)
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
            if (direction.x > 0.0f) onRightSwipe?.Invoke(_parent,_parent.posX + 1);
            else onLeftSwipe?.Invoke(_parent,_parent.posX - 1);
        }
        else if (direction.y > 0.0f) onUpSwipe?.Invoke(_parent, _parent.posY + 1);
        else onDownSwipe?.Invoke(_parent, _parent.posY - 1);
    }

    public void OnDrag(PointerEventData eventData) {}
    private void Update()
    {
        if (_isMoving)
        {
            _ticks += Time.deltaTime;
            var progress = Mathf.Clamp01(_ticks / _duration);
            transform.localPosition = Vector3.Lerp(_currentPos, Vector3.zero, progress);
            if (progress >= 0.99f)
            {
                _currentPos = Vector3.zero;
                _ticks = 0.0f;
                _isMoving = false;
            }
        }   
    }
}
