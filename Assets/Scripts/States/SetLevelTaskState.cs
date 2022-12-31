using Configs;
using UI;

namespace States
{
    public class SetLevelTaskState : State<Core>
    {
        public SetLevelTaskState(Core core) : base(core) {}

        public override void OnEnter()
        {
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
