using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Abilities.Main;
using Player.Components.Stats;

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
            _abilityId = "Dumbbells";
        }

        public void Run()
        {
            foreach (int idx in _initAbilityRequestsFilter)
            {
                var abilityEntity = _initAbilityRequestsFilter.GetEntity(idx);

                if(abilityEntity.Get<PassiveAbilityComponent>().Id != _abilityId)
                    continue;
                
                abilityEntity.Del<UpgradePassiveStatRequest>();

                var abilityData = _loadedData.AbilitiesLibrary.ForPassiveAbility(_abilityId);

                var abilityParameters = abilityData.GetByLevel(0); 
                
                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var projectileDamageStat = ref playerStatsEntity.Get<DamageStat>();

                projectileDamageStat.ModifiedValue = projectileDamageStat.BaseValue + projectileDamageStat.BaseValue / 100 * abilityParameters.Value;
            }
        }
    }
}