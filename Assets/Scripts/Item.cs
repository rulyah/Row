using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public int spriteId;
    public Cell _parentCell;
    
    private Vector3 _movePoint;
    private Vector3 _currentPos;
    private Vector3 _startDragPoint;
    private Vector3 _finishDragPoint;

    private float _tick;
    private float _duration = 0.2f;
    private bool _isMove;
    
    public event Action<int, int> onLeftSwipe;
    public event Action<int, int> onRightSwipe;
    public event Action<int, int> onUpSwipe;
    public event Action<int, int> onDownSwipe;

    

    public void SetParent(Cell cell)
    {
        _parentCell = cell;
        transform.SetParent(cell.transform);
        Move(Vector3.zero);
    }
    
    public void Move(Vector3 movePoint)
    {
        if(_isMove) return;
        _movePoint = movePoint;
        _currentPos = transform.localPosition;
        _isMove = true;
    }
    
    public void Hide()
    {
        spriteId = 10;
        image.color = new Color(255,255,255,0);
    }
    public int GetRandomSpriteId()
    {
        return Random.Range(0, 7);
    }

    public void Show()
    {
        spriteId = GetRandomSpriteId();
        image.color = new Color(255,255,255,100);
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
            if (direction.x > 0.0f) onRightSwipe?.Invoke(_parentCell.buttonId, _parentCell.posX + 1);
            else onLeftSwipe?.Invoke(_parentCell.buttonId, _parentCell.posX - 1);
        }
        else if (direction.y > 0.0f) onUpSwipe?.Invoke(_parentCell.buttonId, _parentCell.posY + 1);
        else onDownSwipe?.Invoke(_parentCell.buttonId, _parentCell.posY - 1);
    }

    public void OnDrag(PointerEventData eventData){}

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
                _parentCell.RefreshItem();
                _isMove = false;
            }
        }   
    }

}
