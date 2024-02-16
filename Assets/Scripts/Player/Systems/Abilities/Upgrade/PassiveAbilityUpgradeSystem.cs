using Data.Loaded;
using Leopotam.Ecs;
using Main.Components;
using Player.Components.Abilities.Main;
using Player.Components.Stats;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Abilities.Upgrade
{
    public class PassiveAbilityUpgradeSystem : IEcsRunSystem
    {
        private EcsWorld _world;
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

                var requestId = requestEntity.Get<InitAbilityRequest>().Id;
                
                foreach (int idx in _initAbilitiesFilter)
                {
                    var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                    ref var passiveAbilityComponent = ref abilityEntity.Get<PassiveAbilityComponent>();

                    if(requestId != passiveAbilityComponent.Id)
                        continue;
                    
                    requestEntity.Del<UpgradeAbilityRequest>();

                    var abilityData = _loadedData.AbilitiesLibrary.ForPassiveAbility(requestId);

                    var parametersData = abilityData.GetByLevel(passiveAbilityComponent.Level);
                
                    passiveAbilityComponent.Id = abilityData.Id;
                    passiveAbilityComponent.Value = parametersData.Value;

                    requestEntity.Get<InitAbilityTag>();
                    requestEntity.Get<UpgradePassiveStatRequest>();
                }
                
                if (_upgradeAbilityRequestFilter.GetEntitiesCount() == 0)
                    _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
            }
        }
    }
}