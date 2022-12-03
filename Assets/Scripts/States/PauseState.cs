using UI;
using UnityEngine;
using Screen = UI.Screen;

namespace States
{
    public class PauseState : State<Core>
    {
        public PauseState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Debug.Log("PauseState");
            _core.uiController.ShowScreen(_core.uiController.pauseScreen);
            Screen.onGameRestart += onGameRestart;
            PauseScreen.onCloseScreenClick += CloseScreen;
        }

        public override void OnExit()
        {
            Screen.onGameRestart -= onGameRestart;
            PauseScreen.onCloseScreenClick -= CloseScreen;
            _core.uiController.CloseLastScreen();
        }

        private void CloseScreen()
        {
            ChangeState(new InputState(_core));
        }
        private void onGameRestart()
        {
            ChangeState(new RestartGameState(_core));
        }
    }
}