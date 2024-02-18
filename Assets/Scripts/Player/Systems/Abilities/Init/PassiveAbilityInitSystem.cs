using Data.Loaded;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using Player.Components.Abilities.Main;
using Player.Components.Stats;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Abilities.Init
{
    public class PassiveAbilityInitSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<InitAbilityRequest> _initAbilityRequestFilter;

        public void Run()
        {
            if (_initAbilityRequestFilter.GetEntitiesCount() == 0)
                return;

            foreach (int idx in _initAbilityRequestFilter)
            {
                var requestEntity = _initAbilityRequestFilter.GetEntity(idx);

                var requestId = requestEntity.Get<InitAbilityRequest>().Id;

                var abilityData = _loadedData.AbilitiesLibrary.ForPassiveAbility(requestId);
                
                if(abilityData == null)
                    return;
                
                ref var passiveAbilityComponent = ref requestEntity.Get<PassiveAbilityComponent>();

                requestEntity.Del<InitAbilityRequest>();

                var parametersData = abilityData.GetByLevel(0);
                
                passiveAbilityComponent.Id = abilityData.Id;
                passiveAbilityComponent.Value = parametersData.Value;

                requestEntity.Get<InitAbilityTag>();
                requestEntity.Get<UpgradePassiveStatRequest>();
                
                _runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Add(passiveAbilityComponent.Id);
                _runTimeData.PlayerChosenAbilitiesData.PlayerPassiveAbilities.Add(passiveAbilityComponent.Id, passiveAbilityComponent.Level);
            }
            
            if(_initAbilityRequestFilter.GetEntitiesCount() == 0)
                _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
        }
    }
}