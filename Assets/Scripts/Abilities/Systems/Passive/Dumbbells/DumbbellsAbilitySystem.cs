using Abilities.Components.Identification.Passive;
using Abilities.Components.Main;
using Data.Enums;
using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Stats;

namespace Abilities.Systems.Passive.Dumbbells
{
    public class DumbbellsAbilitySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private LoadedData _loadedData;
        
        private EcsFilter<InitAbilityTag, DumbbellsAbilityTag, UpgradePassiveStatRequest> _initAbilityRequestsFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        private string _abilityId;

        public void Run()
        {
            if(_initAbilityRequestsFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _initAbilityRequestsFilter)
            {
                var abilityEntity = _initAbilityRequestsFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();
                
                abilityEntity.Del<UpgradePassiveStatRequest>();
                
                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var projectileDamageStat = ref playerStatsEntity.Get<DamageStat>();

                projectileDamageStat.ModifiedValue = projectileDamageStat.BaseValue + projectileDamageStat.BaseValue / 100 * abilityComponent.Value;
            }
        }
    }
}