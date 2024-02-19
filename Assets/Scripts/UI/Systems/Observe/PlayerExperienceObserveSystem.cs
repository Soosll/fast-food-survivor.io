using Data.Static;
using Leopotam.Ecs;
using Player.Components.Experience;
using Player.Components.Main;

namespace UI.Systems.Observe
{
    public class PlayerExperienceObserveSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<PlayerExperienceIncreasedEvent> _increaseExperienceFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;
            
            if(_increaseExperienceFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);
            
            var experiencePanel = _staticData.SceneDependencies.UIDependencies.PlayerHUD.ExperiencePanel;
            
            foreach (int idx in _increaseExperienceFilter)
            {
                var increasedExperienceRequest = _increaseExperienceFilter.GetEntity(idx);
                increasedExperienceRequest.Del<PlayerExperienceIncreasedEvent>();
                
                ref var playerExperienceComponent = ref playerEntity.Get<PlayerExperienceComponent>();

                var progressImage = experiencePanel.Progress;

                progressImage.fillAmount = playerExperienceComponent.CurrentValue / playerExperienceComponent.ToNextLevelValue;
            }
        }
    }
}