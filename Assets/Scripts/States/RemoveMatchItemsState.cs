using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace States
{
    public class RemoveMatchItemsState : State<Core>
    {
        public RemoveMatchItemsState(Core core) : base(core) {}

        //private List<Item> _hiddenItem;
        private int _scoreValue;
        
        public static event Action onScoreChanged;
        public static event Action onGoalAmountChanged;

        public override void OnEnter()
        {
            Debug.Log("MoveItemState");
            //_hiddenItem = new List<Item>();
            _core.StartCoroutine(ChangMatchItemsScale(RemoveItem));
        }

        public override void OnExit()
        {
            onScoreChanged?.Invoke();
        }

        private void RemoveItem()
        {
            for (var i = Model.levelModel.matchList.Count - 1; i >= 0; i--)
            {
                CheckLvlTask(Model.levelModel.matchList[i]);
                Factory.RemoveItemInSlot(Model.levelModel.matchList[i]);
                //_hiddenItem.Add(Model.matchList[i].itemInSlot);
                //Model.matchList[i].RemoveItem();
                Model.levelModel.score += 10;
                ChangeItemInSlots(Model.levelModel.matchList[i]);
            }
            Model.levelModel.matchList.Clear();
            ChangeState(new AddItemsState(_core));
        }
        
        private void ChangeItemInSlots(Slot slot)
        {
            var column = _core.slots.FindAll(n => n.posX == slot.posX && n.posY > slot.posY);
            
            for (var i = 0; i < column.Count; i++)
            {
                var nextSlot = GetSlotByPos(column[i].posX, column[i].posY - 1);
                nextSlot.SetItem(column[i].itemInSlot);
            }
            //column[^1].SetItem(_hiddenItem[0]);
            //SetNewSprite(_hiddenItem[0]);
            //_hiddenItem.Remove(_hiddenItem[0]);
        }

        //private void SetNewSprite(Item item)
        //{
            //item.SetRandomSpriteId();
            //item.image.sprite = _core.sprites[item.spriteId];
        //}

        private Slot GetSlotByPos(int posX, int posY)
        {
            return _core.slots.Find(n => n.posX == posX && n.posY == posY);
        }

        private void CheckLvlTask(Slot slot)
        {
            if (slot.itemInSlot.spriteId == Model.levelModel.currentLevelConfig.firstTaskSpriteId)
            {
                if(Model.levelModel.firstGoalCount > 0) Model.levelModel.firstGoalCount--;
            }
            if (slot.itemInSlot.spriteId == Model.levelModel.currentLevelConfig.secondTaskSpriteId)
            {
                if(Model.levelModel.secondGoalCount > 0) Model.levelModel.secondGoalCount--;
            }
            onGoalAmountChanged?.Invoke();
            if (Model.levelModel.firstGoalCount == 0 && Model.levelModel.secondGoalCount == 0) Model.levelModel.isVictory = true;
        }

        private IEnumerator ChangMatchItemsScale(Action action)
        {
            var scale = new Vector3(0.05f, 0.05f);
            while (Model.levelModel.matchList[0].itemInSlot.transform.localScale.x < 1.3f)
            {
                foreach (var cell in Model.levelModel.matchList)
                {
                    cell.itemInSlot.transform.localScale += scale;
                }
                yield return new WaitForSeconds(0.025f);
            }
            action?.Invoke();
        }
    }
}