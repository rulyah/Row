using Configs;
using UnityEngine;

namespace States
{
    public class CreateSlotsState : State<Core>
    {
        public CreateSlotsState(Core core) : base(core) {}
        
        private int _maskSlotLine = 5;
        private RectTransform _rectTransform;
        private float _slotSizeX;
        private float _slotSizeY;
        
        public override void OnEnter()
        {
            Debug.Log("CreateSlotsState");
            _rectTransform = _core.GetComponent<RectTransform>();
            CalkSlotSize();
            for (var x = 1; x <= GameConfig.gridSize; x++)
            {
                for (var y = 1; y <= GameConfig.gridSize + _maskSlotLine; y++)
                {
                    var slot = _core.fabric.CreateSlot();
                    slot.transform.SetParent(_core.transform);
                    var slotRect = slot.GetComponent<RectTransform>();
                    slotRect.sizeDelta = new Vector2(0.56f, 0.56f);
                    slot.posX = x;
                    slot.posY = y;
                    slot.transform.localPosition = GridPosToLocal(slot.posX, slot.posY);
                    _core.slots.Add(slot);
                    slot.Init();
                }
            }
            ChangeState(new CreateItemsState(_core));
        }
        
        private void CalkSlotSize()
        {
            _slotSizeX = _rectTransform.sizeDelta.x / GameConfig.gridSize;
            _slotSizeY = _rectTransform.sizeDelta.y / GameConfig.gridSize;
        }
        
        public Vector3 GridPosToLocal(int posX, int posY)
        {
            return new Vector3(posX * _rectTransform.sizeDelta.x / GameConfig.gridSize - _rectTransform.sizeDelta.x / 2.0f - _slotSizeX / 2.0f,
                posY * _rectTransform.sizeDelta.x / GameConfig.gridSize - _rectTransform.sizeDelta.x / 2.0f - _slotSizeY / 2.0f);
        }
    }
}