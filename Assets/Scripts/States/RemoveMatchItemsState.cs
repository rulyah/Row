using System;
using System.Collections;
using UnityEngine;

namespace States
{
    public class RemoveMatchItemsState : State<Core>
    {
        public RemoveMatchItemsState(Core core) : base(core) {}

        public static event Action onScoreChanged;
        public static event Action onGoalAmountChanged;

        public override void OnEnter()
        {
            _core.StartCoroutine(ChangMatchItemsScale(() =>
            {
                for (var i = 0; i < Model.levelModel.matchList.Count; i++)
                {
                    CheckLvlTask(Model.levelModel.matchList[i]);
                    Factory.RemoveItem(Model.levelModel.matchList[i].itemInSlot);
                    Model.levelModel.matchList[i].itemInSlot = null;
                    Model.levelModel.score += 10;
                }
                ChangeState(new MoveToMissingState(_core));
            }));
        }

        public override void OnExit()
        {
            onScoreChanged?.Invoke();
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