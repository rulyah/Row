using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private Item _itemPrefab;
    
    public Slot CreateSlot()
    {
        return Instantiate(_slotPrefab);
    }

    public Item CreateItem()
    {
        return Instantiate(_itemPrefab);
    }
}