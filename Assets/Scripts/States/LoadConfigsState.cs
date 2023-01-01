using Configs;
using UnityEngine;

namespace States
{
    public class LoadConfigsState : State<Core>
    {
        public LoadConfigsState(Core core) : base(core) {}

        public override void OnEnter()
        {
            Model.levelModel = new LevelModel();
            GameConfig.spritesConfig = Resources.Load<SpritesConfig>("Configs/SpritesConfig/SpritesConfig");
            GameConfig.soundsConfig = Resources.Load<SoundsConfig>("Configs/SoundsConfig/SoundConfig");
            GameConfig.levelConfig = Resources.LoadAll<LevelConfig>("Configs/LevelsConfig");
            GameConfig.slotPrefab = Resources.Load<Slot>("Prefabs/Slot");
            GameConfig.itemPrefab = Resources.Load<Item>("Prefabs/Item");
            Model.currentLevel = GameConfig.levelConfig[0].levelId;
            ChangeState(new CreateSlotsState(_core));
        }
    }
}