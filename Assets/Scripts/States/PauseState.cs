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
            UIController.instance.ShowScreen(UIController.instance.pauseScreen);
            Screen.onGameRestart += onGameRestart;
            PauseScreen.onCloseScreenClick += CloseScreen;
        }

        public override void OnExit()
        {
            Screen.onGameRestart -= onGameRestart;
            PauseScreen.onCloseScreenClick -= CloseScreen;
            UIController.instance.CloseLastScreen();
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