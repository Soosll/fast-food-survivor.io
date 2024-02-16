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

                ref var playerExperience = ref playerEntity.Get<PlayerExperienceComponent>().Value;
                ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>().Level;

                var experienceToTheNextLevel = _loadedData.LevelUpLibrary.GetExperienceByLevel(playerLevel + 1);

                if (playerExperience >= experienceToTheNextLevel)
                    playerEntity.Get<PlayerLevelUpRequest>();
            }
        }
    }
}