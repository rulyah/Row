using Configs;
using UnityEngine;

namespace States
{
    public class RestartGameState : State<Core>
    {
        public RestartGameState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("RestartGameState");
            ChangeState(new SetLevelTaskState(_core));
        }
    }
}