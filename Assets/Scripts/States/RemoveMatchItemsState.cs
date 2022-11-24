using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class RemoveMatchItemsState : State<Core>
    {
        public RemoveMatchItemsState(Core core) : base(core) {}

        private List<Item> _hiddenItem;
        private int _scoreValue;
        
        public static event Action<int> onScoreChanged;
        public static event Action<int> onGoalAmountChanged;
        //public static event Action onSecondGoalAmountChanged;

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
            _hiddenItem.Remove(_hiddenItem[0]);
        }// add random to _hiddenItem

        private Slot GetSlotByPos(int posX, int posY)
        {
            return _core.slots.Find(n => n.posX == posX && n.posY == posY);
        }

        private void CheckLvlTask(Slot slot)
        {
            if (slot.itemInSlot.spriteId == Model.firstGoalSpriteId)
            {
                onGoalAmountChanged?.Invoke(1);
            }
            if (slot.itemInSlot.spriteId == Model.secondGoalSpriteId)
            {
                onGoalAmountChanged?.Invoke(2);
            }
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