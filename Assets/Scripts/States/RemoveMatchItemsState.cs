using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace States
{
    public class RemoveMatchItemsState : State<Core>
    {
        public RemoveMatchItemsState(Core core) : base(core) {}

        private List<Item> _hiddenItem;
        private int _scoreValue;
        
        public static event Action<int> onScoreChanged;
        public static event Action onGoalAmountChanged;

        public override void OnEnter()
        {
            Debug.Log("MoveItemState");
            _hiddenItem = new List<Item>();
            _core.StartCoroutine(ChangMatchItemsScale(RemoveItem));
        }

        public override void OnExit()
        {
            onScoreChanged?.Invoke(_scoreValue);
        }

        private void RemoveItem()
        {
            for (var i = Model.matchList.Count - 1; i >= 0; i--)
            {
                CheckLvlTask(Model.matchList[i]);
                _hiddenItem.Add(Model.matchList[i].itemInSlot);
                Model.matchList[i].RemoveItem();
                _scoreValue += 10;
                ChangeItemInSlots(Model.matchList[i]);
            }
            Model.matchList.Clear();
            ChangeState(new MoveItemsState(_core));
        }
        
        private void ChangeItemInSlots(Slot slot)
        {
            var column = _core.slots.FindAll(n => n.posX == slot.posX && n.posY > slot.posY);
            
            for (var i = 0; i < column.Count; i++)
            {
                var nextSlot = GetSlotByPos(column[i].posX, column[i].posY - 1);
                nextSlot.SetItem(column[i].itemInSlot);
            }
            column[^1].SetItem(_hiddenItem[0]);
            SetNewSprite(_hiddenItem[0]);
            _hiddenItem.Remove(_hiddenItem[0]);
        }

        private void SetNewSprite(Item item)
        {
            item.SetRandomSpriteId();
            item.image.sprite = _core.sprites[item.spriteId];
        }

        private Slot GetSlotByPos(int posX, int posY)
        {
            return _core.slots.Find(n => n.posX == posX && n.posY == posY);
        }

        private void CheckLvlTask(Slot slot)
        {
            if (slot.itemInSlot.spriteId == Model.firstGoalSpriteId)
            {
                if(Model.firstGoalCount > 0) Model.firstGoalCount--;
            }
            if (slot.itemInSlot.spriteId == Model.secondGoalSpriteId)
            {
                if(Model.secondGoalCount > 0) Model.secondGoalCount--;
            }
            onGoalAmountChanged?.Invoke();
        }

        private IEnumerator ChangMatchItemsScale(Action action)
        {
            var scale = new Vector3(0.05f, 0.05f);
            while (Model.matchList[0].itemInSlot.transform.localScale.x < 1.3f)
            {
                foreach (var cell in Model.matchList)
                {
                    cell.itemInSlot.transform.localScale += scale;
                }
                yield return new WaitForSeconds(0.05f);
            }
            action?.Invoke();
        }
    }
}