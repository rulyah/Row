using Configs;
using UI;
using Screen = UI.Screen;

namespace States
{
    public class VictoryState : State<Core>
    {
        public VictoryState(Core core) : base(core) {}

        public override void OnEnter()
        {
            AudioController.instance.Play(GameConfig.soundsConfig.victorySound);
            UIController.instance.ShowScreen(UIController.instance.completeScreen);
            CompleteScreen.onNextLevelClick += OnNextLevelClick;
            Screen.onGameRestart += onGameRestart;
        }

        public override void OnExit()
        {
            CompleteScreen.onNextLevelClick -= OnNextLevelClick;
            Screen.onGameRestart -= onGameRestart;
            UIController.instance.CloseLastScreen();
            Model.levelModel.isVictory = false;
        }

        private void OnNextLevelClick()
        {
            Model.currentLevel++;
            Model.levelModel.score = 0;
            Model.levelModel.movesCount = 0;
            UIController.instance.GameRestart();
            ChangeState(new SetLevelTaskState(_core));
        }

        private void onGameRestart()
        {
            ChangeState(new RestartGameState(_core));
        }
    }
}