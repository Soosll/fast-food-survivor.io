using Data.Loaded;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using Player.Components.Abilities.Main;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Abilities.Init
{
    public class ActiveAbilityInitSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<InitAbilityRequest> _initAbilityRequestFilter;
        
        public void Run()
        {
            if(_initAbilityRequestFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _initAbilityRequestFilter)
            {
                var requestEntity = _initAbilityRequestFilter.GetEntity(idx);
                
                var requestId = requestEntity.Get<InitAbilityRequest>().Id;
                
                var abilityData = _loadedData.AbilitiesLibrary.ForActiveAbility(requestId);
                
                if(abilityData == null)
                    return;

                requestEntity.Del<InitAbilityRequest>();

                var abilityParameters = abilityData.GetByLevel(0);
                
                var abilityEntity = _world.NewEntity();
                ref var activeAbilityComponent = ref abilityEntity.Get<PlayerActiveAbilityComponent>();
                
                activeAbilityComponent.Id = abilityData.Id;
                activeAbilityComponent.Amount = abilityParameters.Amount;
                activeAbilityComponent.Area = abilityParameters.Area;
                activeAbilityComponent.Cooldown = abilityParameters.Cooldown;
                activeAbilityComponent.Damage = abilityParameters.Damage;
                activeAbilityComponent.Duration = abilityParameters.Duration;
                activeAbilityComponent.Knockback = abilityParameters.Knockback;
                activeAbilityComponent.Pierce = abilityParameters.Pierce;
                activeAbilityComponent.Speed = abilityParameters.Speed;
                activeAbilityComponent.CritChance = abilityParameters.CritChance;
                activeAbilityComponent.CritMultiplyer = abilityParameters.CritMultiplyer;
                activeAbilityComponent.HitboxDelay = abilityParameters.HitboxDelay;
                activeAbilityComponent.ProjectilePrefab = abilityData.ProjectilePrefab;
                abilityEntity.Get<InitAbilityTag>();
                
                _runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Add(activeAbilityComponent.Id);
                _runTimeData.PlayerChosenAbilitiesData.PlayerActiveAbilities.Add(activeAbilityComponent.Id, activeAbilityComponent.Level);
            }
            
            if(_initAbilityRequestFilter.GetEntitiesCount() == 0)
                _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
        }
    }
}