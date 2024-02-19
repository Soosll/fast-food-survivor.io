using Abilities.Components.Identification.Passive;
using Abilities.Components.Main;
using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Stats;
using UnityEngine;

namespace Abilities.Systems.Passive.FastSneakers
{
    public class FastSneakersAbilitySystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;
        
        private EcsFilter<InitAbilityTag, UpgradePassiveStatRequest> _initAbilitiesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        public void Run()
        {
            if(_initAbilitiesFilter.GetEntitiesCount() == 0)
                return;
            
            Debug.Log("Нашел ботинки");
            
            foreach (int idx in _initAbilitiesFilter)
            {
                var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();

                abilityEntity.Del<UpgradePassiveStatRequest>();
                
                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var playerMoveSpeedStat = ref playerStatsEntity.Get<MoveSpeedStat>();

                playerMoveSpeedStat.ModifiedValue = playerMoveSpeedStat.BaseValue + playerMoveSpeedStat.BaseValue / 100 * abilityComponent.Value;
            }
        }
    }
}