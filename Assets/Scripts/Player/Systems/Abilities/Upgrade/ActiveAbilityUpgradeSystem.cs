using System.Linq;
using Data.Loaded;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using Player.Components.Abilities.Main;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Abilities.Upgrade
{
    public class ActiveAbilityUpgradeSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<UpgradeAbilityRequest> _upgradeAbilitiesRequestFilter;
        private EcsFilter<InitAbilityTag> _initAbilitiesFilter;

        public void Run()
        {
            if (_upgradeAbilitiesRequestFilter.GetEntitiesCount() == 0)
                return;

            foreach (int i in _upgradeAbilitiesRequestFilter)
            {
                var requestEntity = _upgradeAbilitiesRequestFilter.GetEntity(i);
                var requestId = requestEntity.Get<UpgradeAbilityRequest>().Id;

                foreach (int idx in _initAbilitiesFilter)
                {
                    var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                    ref var abilityComponent = ref abilityEntity.Get<PlayerActiveAbilityComponent>();
                    
                    if (abilityComponent.Id != requestId)
                        continue;

                    requestEntity.Del<UpgradeAbilityRequest>();
                    
                    abilityComponent.Level++;
                    
                    var abilityData = _loadedData.AbilitiesLibrary.ForActiveAbility(requestId);

                    var abilityParameters = abilityData.GetByLevel(abilityComponent.Level);

                    abilityComponent.Id = abilityData.Id;
                    abilityComponent.Amount = abilityParameters.Amount;
                    abilityComponent.Area = abilityParameters.Area;
                    abilityComponent.Cooldown = abilityParameters.Cooldown;
                    abilityComponent.Damage = abilityParameters.Damage;
                    abilityComponent.Duration = abilityParameters.Duration;
                    abilityComponent.Knockback = abilityParameters.Knockback;
                    abilityComponent.Pierce = abilityParameters.Pierce;
                    abilityComponent.Speed = abilityParameters.Speed;
                    abilityComponent.CritChance = abilityParameters.CritChance;
                    abilityComponent.CritMultiplyer = abilityParameters.CritMultiplyer;
                    abilityComponent.HitboxDelay = abilityParameters.HitboxDelay;
                    abilityComponent.ProjectilePrefab = abilityData.ProjectilePrefab;
                    
                    _runTimeData.PlayerChosenAbilitiesData.PlayerActiveAbilities[abilityComponent.Id] = abilityComponent.Level;
                    
                    if (abilityComponent.Level == abilityData.MaxLevel - 1)
                    {
                        _loadedData.AbilitiesLibrary.RemoveActiveElement(abilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.PlayerActiveAbilities.Remove(abilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Remove(abilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.FullyUpgradedSpellsCount++;
                    }
                }
            }
            
            if (_upgradeAbilitiesRequestFilter.GetEntitiesCount() == 0)
                _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
        }
    }
}