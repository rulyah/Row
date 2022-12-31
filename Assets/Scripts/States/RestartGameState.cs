namespace States
{
    public class RestartGameState : State<Core>
    {
        public RestartGameState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Model.levelModel.score = 0;
            Model.levelModel.movesCount = 0;
            ChangeState(new SetLevelTaskState(_core));
        }
    }
}