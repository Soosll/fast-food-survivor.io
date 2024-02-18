using Data.Enums;
using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Abilities.Main;
using Player.Components.Stats;
using UnityEngine;

namespace Player.Systems.Abilities.Passive.Dumbbells
{
    public class DumbbellsAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private LoadedData _loadedData;
        
        private EcsFilter<InitAbilityTag, UpgradePassiveStatRequest> _initAbilityRequestsFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        private string _abilityId;
        
        public void Init()
        {
            _abilityId = AbilitiesId.Dumbbells.ToString();
        }

        public void Run()
        {
            if(_initAbilityRequestsFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _initAbilityRequestsFilter)
            {
                var abilityEntity = _initAbilityRequestsFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();
                
                if(abilityComponent.Id != _abilityId)
                    continue;
                
                abilityEntity.Del<UpgradePassiveStatRequest>();
                
                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var projectileDamageStat = ref playerStatsEntity.Get<DamageStat>();

                projectileDamageStat.ModifiedValue = projectileDamageStat.BaseValue + projectileDamageStat.BaseValue / 100 * abilityComponent.Value;
            }
        }
    }
}