using Configs;
using UI;
using UnityEngine;
using Screen = UI.Screen;

namespace States
{
    public class VictoryState : State<Core>
    {
        public VictoryState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("VictoryState");
            _core.uiController.ShowScreen(_core.uiController.completeScreen);
            CompleteScreen.onNextLevelClick += OnNextLevelClick;
            Screen.onGameRestart += onGameRestart;
        }

        public override void OnExit()
        {
            CompleteScreen.onNextLevelClick -= OnNextLevelClick;
            Screen.onGameRestart -= onGameRestart;
            _core.uiController.CloseLastScreen();
            GameConfig.isVictory = false;
        }

        private void OnNextLevelClick()
        {
            GameConfig.currentLevel++;
            ChangeState(new SetLevelTaskState(_core, GameConfig.currentLevel));
        }

        private void onGameRestart()
        {
            ChangeState(new RestartGameState(_core));
        }
    }
}