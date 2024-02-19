using Abilities.Components.Main;
using Data.Enums;
using Data.Loaded;
using Leopotam.Ecs;
using Player.Components.Stats;

namespace Abilities.Systems.Passive.FastSneakers
{
    public class FastSneakersAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;
        
        private EcsFilter<InitAbilityTag, UpgradePassiveStatRequest> _initAbilitiesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        private string _abilityId;

        public void Init()
        {
            _abilityId = AbilitiesId.FastSneakers.ToString();
        }

        public void Run()
        {
            if(_initAbilitiesFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _initAbilitiesFilter)
            {
                var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();

                if (abilityComponent.Id != _abilityId)
                    continue;

                abilityEntity.Del<UpgradePassiveStatRequest>();
                
                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var playerMoveSpeedStat = ref playerStatsEntity.Get<MoveSpeedStat>();

                playerMoveSpeedStat.ModifiedValue = playerMoveSpeedStat.BaseValue + playerMoveSpeedStat.BaseValue / 100 * abilityComponent.Value;
            }
        }
    }
}