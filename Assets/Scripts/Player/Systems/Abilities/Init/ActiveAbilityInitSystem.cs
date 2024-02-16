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

        private EcsFilter<InitAbilityRequest> _initRequestFilter;
        
        public void Run()
        {
            if(_initRequestFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _initRequestFilter)
            {
                var requestEntity = _initRequestFilter.GetEntity(idx);
                
                var requestId = requestEntity.Get<InitAbilityRequest>().Id;
                
                var abilityData = _loadedData.AbilitiesLibrary.ForActiveAbility(requestId);
                
                if(abilityData == null)
                    return;

                requestEntity.Del<InitAbilityRequest>();

                var abilityParameters = abilityData.GetByLevel(0);
                
                var abilityEntity = _world.NewEntity();
                ref var abilityComponent = ref abilityEntity.Get<PlayerActiveAbilityComponent>();
                
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
                abilityEntity.Get<InitAbilityTag>();
                
                _runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Add(abilityComponent.Id);
                _runTimeData.PlayerChosenAbilitiesData.PlayerActiveAbilities.Add(abilityComponent.Id, abilityComponent.Level);
            }
            
            if(_initRequestFilter.GetEntitiesCount() == 0)
                _world.NewEntityWith<AfterSpellChooseContinueGameRequest>();
        }
    }
}