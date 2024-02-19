using Abilities.Components.Main;
using Leopotam.Ecs;
using Player.Components.Experience;
using Player.Components.Main;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Experience
{
    public class PlayerLevelUpSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<InitPlayerTag, PlayerLevelUpRequest> _levelUpPlayersFilter;

        public void Run()
        {
            if(_levelUpPlayersFilter.GetEntitiesCount() == 0)
                return;
            
            var playerEntity = _levelUpPlayersFilter.GetEntity(0);

            playerEntity.Del<PlayerLevelUpRequest>();

            playerEntity.Get<PlayerExperienceComponent>().Value = 0;
            
            ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>().Level;

            playerLevel++;

            _world.NewEntityWith<ChooseAbilityRequest>();
        }
    }
}