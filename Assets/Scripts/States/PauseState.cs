using UI;
using Screen = UI.Screen;

namespace States
{
    public class PauseState : State<Core>
    {
        public PauseState(Core core) : base(core) {}

        public override void OnEnter()
        {
            UIController.instance.ShowScreen(UIController.instance.pauseScreen);
            Screen.onGameRestart += onGameRestart;
            PauseScreen.onCloseScreenClick += CloseScreen;
            //AudioController.instance.Init();
        }

        public override void OnExit()
        {
            Screen.onGameRestart -= onGameRestart;
            PauseScreen.onCloseScreenClick -= CloseScreen;
            UIController.instance.CloseLastScreen();
            //AudioController.instance.DeInit();
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