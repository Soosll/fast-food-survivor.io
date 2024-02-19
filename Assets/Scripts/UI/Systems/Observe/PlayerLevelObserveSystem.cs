using Data.Static;
using Leopotam.Ecs;
using Player.Components.Experience;
using Player.Components.Main;

namespace UI.Systems.Observe
{
    public class PlayerLevelObserveSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;
        
        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<PlayerLevelUpEvent> _levelUpFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;
            
            if(_levelUpFilter.GetEntitiesCount() == 0)
                return;

            var levelPanel = _staticData.SceneDependencies.UIDependencies.PlayerHUD.LevelPanel;
            
            foreach (var idx in _levelUpFilter)
            {
                var playerEntity = _playersFilter.GetEntity(0);
                
                var levelUpRequest = _levelUpFilter.GetEntity(idx);
                levelUpRequest.Del<PlayerLevelUpEvent>();

                ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>().Level;

                levelPanel.Level.text = $"Level: {playerLevel.ToString()}";
            }
        }
    }
}