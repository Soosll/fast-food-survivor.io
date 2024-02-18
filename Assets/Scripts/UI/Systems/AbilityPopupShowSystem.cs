using Data.Enums;
using Data.Loaded;
using Data.RunTime;
using Data.Static;
using Data.Static.Abilities;
using Drop.Components;
using Leopotam.Ecs;
using Player.Components.Experience;
using UI.Presenters;
using UnityEngine;

namespace UI.Systems
{
    public class AbilityPopupShowSystem : IEcsRunSystem // имеет доступ к UI попапу и создает презентер
    {
        private EcsWorld _world;
        
        private StaticData _staticData;
        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<ChosenRandomAbilityFromPoolRequest> _chosenAbilityRequestFilter;

        public void Run()
        {
            if(_chosenAbilityRequestFilter.GetEntitiesCount() == 0)
                return;

            _runTimeData.InGameData.GamePhase = GamePhase.SpellChoose;
            
            foreach (int idx in _chosenAbilityRequestFilter)
            {
                var entity = _chosenAbilityRequestFilter.GetEntity(idx);

               ref var chosenAbilityRequest = ref entity.Get<ChosenRandomAbilityFromPoolRequest>();
                
                var randomAbilityId = entity.Get<ChosenRandomAbilityFromPoolRequest>().ChosenAbilityId;
                
                entity.Del<ChosenRandomAbilityFromPoolRequest>();
                
                var popupsHolder = _staticData.SceneDependencies.UIDependencies.AbilitiesPopupHolder;
                var playerChosenAbilities = _runTimeData.PlayerChosenAbilitiesData;
               
                var activeAbilityData = _loadedData.AbilitiesLibrary.ForActiveAbility(randomAbilityId);

                if (activeAbilityData != null)
                {
                    if (playerChosenAbilities.PlayerActiveAbilities.ContainsKey(randomAbilityId))
                    {
                        var abilityLevel = playerChosenAbilities.GetActiveAbilityLevelById(randomAbilityId);
                        var presenter = GetActiveAbilityPresenter(activeAbilityData, abilityLevel + 1);
                        popupsHolder.Show(presenter);
                        continue;
                    }

                    else
                    {
                        var presenter = GetActiveAbilityPresenter(activeAbilityData, 0);
                        popupsHolder.Show(presenter);
                        continue;
                    }
                }

                var passiveAbilityData = _loadedData.AbilitiesLibrary.ForPassiveAbility(randomAbilityId);

                if (passiveAbilityData != null)
                {
                    if (playerChosenAbilities.PlayerPassiveAbilities.ContainsKey(randomAbilityId))
                    {
                        var abilityLevel = playerChosenAbilities.GetPassiveAbilityLevelById(randomAbilityId);
                        var presenter = GetPassiveAbilityPresenter(passiveAbilityData, abilityLevel + 1);
                        popupsHolder.Show(presenter);
                    }

                    else
                    {
                        var presenter = GetPassiveAbilityPresenter(passiveAbilityData, 0);
                        popupsHolder.Show(presenter);
                    }
                }
            }
        }

        private IAbilityPresenter GetActiveAbilityPresenter(ActiveAbilityData abilityData, int level)
        {
            var presenter = new AbilityPresenter();

            var abilityParameters = abilityData.GetByLevel(level);
            
            presenter.AbilityDescription = abilityParameters.Description;
            presenter.AbilityLevel = level;
            presenter.AbilityName = abilityData.Id;
            presenter.AbilitySprite = abilityData.AbilitySprite;
            
            return presenter;
        }
        
        private IAbilityPresenter GetPassiveAbilityPresenter(PassiveAbilityData abilityData, int level)
        {
            var presenter = new AbilityPresenter();

            var abilityParameters = abilityData.GetByLevel(level);
            
            presenter.AbilityDescription = abilityParameters.Description;
            presenter.AbilityLevel = level;
            presenter.AbilityName = abilityData.Id;
            presenter.AbilitySprite = abilityData.AbilitySprite;
            
            return presenter;
        }
    }
}