using Abilities.Components.Identification.Passive;
using Abilities.Components.Main;
using Data.Enums;
using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Stats;

namespace Abilities.Systems.Passive.VitaminB
{
    public class VitaminBAbilitySystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;
        
        private EcsFilter<InitAbilityTag, VitaminBAbilityTag, UpgradePassiveStatRequest> _initAbilitiesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        public void Run()
        {
            if(_initAbilitiesFilter.GetEntitiesCount() == 0)
                return;
            
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            foreach (int idx in _initAbilitiesFilter)
            {
                var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();
                
                abilityEntity.Del<UpgradePassiveStatRequest>();
                
                ref var projectileSpeedStat = ref playerStatsEntity.Get<ProjectilesSpeedStat>();

               projectileSpeedStat.ModifiedValue = projectileSpeedStat.BaseValue + projectileSpeedStat.BaseValue / 100 * abilityComponent.Value;
            }
        }
    }
}