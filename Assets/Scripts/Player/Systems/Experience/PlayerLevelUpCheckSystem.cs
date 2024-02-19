using Data.Loaded;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Experience;
using Player.Components.Main;
using UnityEngine;

namespace Player.Systems.Experience
{
    public class PlayerLevelUpCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private LoadedData _loadedData;

        private EcsFilter<InitPlayerTag> _playersFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _playersFilter)
            {
                var playerEntity = _playersFilter.GetEntity(0);

                ref var playerExperienceComponent = ref playerEntity.Get<PlayerExperienceComponent>();
                ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>().Level;

                var experienceToTheNextLevel = _loadedData.LevelUpLibrary.GetExperienceToNextLevel(playerLevel);
                playerExperienceComponent.ToNextLevelValue = experienceToTheNextLevel;
                
                if (playerExperienceComponent.CurrentValue >= playerExperienceComponent.ToNextLevelValue)
                    playerEntity.Get<PlayerLevelUpRequest>();
            }
        }
    }
}