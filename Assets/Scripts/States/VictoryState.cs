using System;
using Configs;
using UI;
using UnityEngine.SceneManagement;

namespace States
{
    public class VictoryState : State<Core>
    {
        public VictoryState(Core core) : base(core) {}

        public static event Action onVictory;
        public override void OnEnter()
        {
            CompleteScreen.onNextLevelClick += OnNextLevelClick;
            onVictory?.Invoke();
        }

        public override void OnExit()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnNextLevelClick()
        {
            GameConfig.currentLevel++;
        }
    }
}