using System;
using Configs;
using UnityEngine;

namespace States
{
    public class SetLevelTaskState : State<Core>
    {
        private readonly int _level;

        public SetLevelTaskState(Core core, int level) : base(core)
        {
            _level = level;
        }

        public static event Action<LevelConfig> onSetTask;
        public override void OnEnter()
        {
            Debug.Log("SetLevelTaskState");
            var config = Resources.Load<LevelConfig>($"LevelsConfig/{_level.ToString()}LevelConfig");
            onSetTask?.Invoke(config);
            _core.uiController.SetTask(_core.sprites);//
            ChangeState(new InputItemsValueState(_core));
        }
    }
}
