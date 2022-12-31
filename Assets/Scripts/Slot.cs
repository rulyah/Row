using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    public Item itemInSlot;
    public int posX;
    public int posY;
    
    public event Action<Slot> onButtonClick;
    
    public void MoveItemToSlot()
    {
        itemInSlot.Move(this);
    }
    
    public void Init()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void SetItem(Item item)
    {
        itemInSlot = item;
    }
    
    private void OnClick()
    {
        onButtonClick?.Invoke(this);
    }

    public void HideSlot()
    {
        _button.interactable = false;
    }
}