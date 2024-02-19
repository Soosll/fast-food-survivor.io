using Abilities.Components.Main;
using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using UI.Components;
using Zun010.LeoEcsExtensions;

namespace Abilities.Systems
{
    public class CreateChosenAbilitySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private EcsFilter<AbilityChooseEvent> _chosenAbilitiesFilter;

        public void Run()
        {
            foreach (int idx in _chosenAbilitiesFilter)
            {
                var chosenAbilityEntity = _chosenAbilitiesFilter.GetEntity(idx);

                var abilityId = chosenAbilityEntity.Get<AbilityChooseEvent>().ChosenAbilityId;

                chosenAbilityEntity.Del<AbilityChooseEvent>();
                
                if (_runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Contains(abilityId))
                {
                    var upgradeAbilityRequest = _world.NewEntityWith<UpgradeAbilityRequest>();
                    upgradeAbilityRequest.Get<UpgradeAbilityRequest>().Id = abilityId;
                }

                else
                {
                    var initAbilityRequest = _world.NewEntityWith<InitAbilityRequest>();
                    initAbilityRequest.Get<InitAbilityRequest>().Id = abilityId;
                }

            }
        }
    }
}