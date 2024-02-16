using Data.Loaded;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Abilities.Main;
using Player.Components.Main;
using Player.Components.Stats;

namespace Player.Systems.Abilities.Passive.FastSneakers
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
            _abilityId = "FastSneakers";
        }

        public void Run()
        {
            foreach (int idx in _initAbilitiesFilter)
            {
                var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                if (abilityEntity.Get<PassiveAbilityComponent>().Id != _abilityId)
                    continue;

                abilityEntity.Del<UpgradePassiveStatRequest>();

                var abilityParameters = _loadedData.AbilitiesLibrary.ForPassiveAbility(_abilityId).GetByLevel(0);

                var playerStatsEntity = _playerStatsFilter.GetEntity(0);

                ref var playerMoveSpeedStat = ref playerStatsEntity.Get<MoveSpeedStat>();

                playerMoveSpeedStat.ModifiedValue = playerMoveSpeedStat.BaseValue + playerMoveSpeedStat.BaseValue / 100 * abilityParameters.Value;
            }
        }
    }
}