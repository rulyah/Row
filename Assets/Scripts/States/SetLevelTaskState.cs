using System;
using Configs;
using UnityEngine;

namespace States
{
    public class SetLevelTaskState : State<Core>
    {
        public SetLevelTaskState(Core core) : base(core) {}

        public static event Action<LevelConfig> onSetTask;
        public override void OnEnter()
        {
            Debug.Log("SetLevelTaskState");
            var config = Resources.Load<LevelConfig>("LevelsConfig/1LevelConfig");
            onSetTask?.Invoke(config);
            _core.uiController.SetTask(_core.sprites);
            ChangeState(new CreateSlotsState(_core));
        }
    }
}
