using Abilities.Components.Main;
using Data.Loaded;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using Player.Components.Stats;
using Zun010.LeoEcsExtensions;

namespace Abilities.Systems.Upgrade
{
    public class PassiveAbilityUpgradeSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<UpgradeAbilityRequest> _upgradeAbilityRequestFilter;
        private EcsFilter<InitAbilityTag> _initAbilitiesFilter;

        public void Run()
        {
            if (_upgradeAbilityRequestFilter.GetEntitiesCount() == 0)
                return;

            foreach (int i in _upgradeAbilityRequestFilter)
            {
                var requestEntity = _upgradeAbilityRequestFilter.GetEntity(i);
                var requestId = requestEntity.Get<UpgradeAbilityRequest>().Id;
                
                foreach (int idx in _initAbilitiesFilter)
                {
                    var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                    ref var passiveAbilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();

                    if(passiveAbilityComponent.Id != requestId)
                        continue;
                    
                    requestEntity.Del<UpgradeAbilityRequest>();

                    passiveAbilityComponent.Level++;

                    var abilityData = _loadedData.AbilitiesLibrary.ForPassiveAbility(requestId);
                    
                    var abilityParameters = abilityData.GetByLevel(passiveAbilityComponent.Level);
                
                    passiveAbilityComponent.Id = abilityData.Id;
                    passiveAbilityComponent.Value = abilityParameters.Value;

                    abilityEntity.Get<UpgradePassiveStatRequest>();
                    
                    _runTimeData.PlayerChosenAbilitiesData.PlayerPassiveAbilities[passiveAbilityComponent.Id] = passiveAbilityComponent.Level;
                    
                    if (passiveAbilityComponent.Level == abilityData.MaxLevel - 1)
                    {
                        _loadedData.AbilitiesLibrary.RemovePassiveElement(passiveAbilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.PlayerPassiveAbilities.Remove(passiveAbilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Remove(passiveAbilityComponent.Id);
                        _runTimeData.PlayerChosenAbilitiesData.FullyUpgradedSpellsCount++;
                    }
                }
            }
            
            if (_upgradeAbilityRequestFilter.GetEntitiesCount() == 0)
                _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
        }
    }
}