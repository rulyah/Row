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
    
    public void RemoveItem()
    {
        //Destroy(itemInSlot.gameObject);
        itemInSlot.HideItem();
        itemInSlot = null;
    }

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

    public void SetNonInteractable()
    {
        _button.interactable = false;
    }
}