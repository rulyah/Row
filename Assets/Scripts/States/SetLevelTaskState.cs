using Configs;
using UI;
using UnityEngine;

namespace States
{
    public class SetLevelTaskState : State<Core>
    {

        public SetLevelTaskState(Core core) : base(core) {}

        public override void OnEnter()
        {
            //Model.score = 0;/////
            //Model.movesCount = 0;//////
            Debug.Log("SetLevelTaskState");
            Model.levelModel.currentLevelConfig = GameConfig.levelConfig[Model.currentLevel];
            SetLevelTask(Model.levelModel.currentLevelConfig);
            UIController.instance.SetTask();
            ChangeState(new InputItemsValueState(_core));
        }

        private void SetLevelTask(LevelConfig levelConfig)
        {
            Model.levelModel.firstGoalCount = levelConfig.firstTaskCount;
            Model.levelModel.secondGoalCount = levelConfig.secondTaskCount;
        }
    }
}
