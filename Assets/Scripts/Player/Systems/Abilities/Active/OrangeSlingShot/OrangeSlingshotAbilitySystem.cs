using Data.Enums;
using Data.Loaded;
using Enemy.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Abilities.Main;
using Player.Components.Main;
using Projectiles.Components.General;
using Projectiles.Components.Throw;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;
using DamageComponent = General.Components.Parameters.DamageComponent;

namespace Player.Systems.Abilities.Active.OrangeSlingShot
{
    public class OrangeSlingshotAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<InitEnemyTag> _enemiesFilter;
        private EcsFilter<InitAbilityTag>.Exclude<CooldownComponent> _playerAbilitiesFilter;

        private string _abilityId;
        
        public void Init()
        {
            _abilityId = AbilitiesId.OrangeSlingShot.ToString();
        }
        
        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;
            
            if(_enemiesFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _playerAbilitiesFilter)
            {
                if(_playerAbilitiesFilter.GetEntitiesCount() == 0)
                    return;
                
                var abilityEntity = _playerAbilitiesFilter.GetEntity(idx);

                var abilityComponent = abilityEntity.Get<PlayerActiveAbilityComponent>();

                if(abilityComponent.Id != _abilityId)
                    continue;

                var playerEntity = _playersFilter.GetEntity(0);
                ref var playerEntityTransform = ref playerEntity.Get<TransformLink>();
                ref var playerClosestTargetDirection = ref playerEntity.Get<ClosestTargetDirectionComponent>().Position;
                
                for (int i = 0; i < abilityComponent.Amount; i++)
                {
                    var projectileEntity =_world.NewEntityWith<ProjectileTag>();

                    projectileEntity.Get<ProjectileTag>().Id = abilityComponent.Id;
                    projectileEntity.Get<ThrowProjectileTag>();
                    projectileEntity.Get<MoveComponent>().Value = abilityComponent.Speed;
                    projectileEntity.Get<MoveDirectionComponent>().Direction = playerClosestTargetDirection.normalized;
                    projectileEntity.Get<DurationComponent>().Value = abilityComponent.Duration;
                    projectileEntity.Get<PierceComponent>().Value = abilityComponent.Pierce;
                    projectileEntity.Get<DamageComponent>().Value = abilityComponent.Damage;
                    projectileEntity.Get<HitBoxDelayComponent>().Value = abilityComponent.HitboxDelay;
                    projectileEntity.Get<KnockbackComponent>().Value = abilityComponent.Knockback;

                    ref var projectilePrefab = ref abilityComponent.ProjectilePrefab;

                    var monoPrefab = Object.Instantiate(projectilePrefab, playerEntityTransform.Transform.position, Quaternion.identity);
                    
                    monoPrefab.Make(ref projectileEntity);
                }

                abilityEntity.Get<CooldownComponent>().Value = abilityComponent.Cooldown;
            }
        }
    }
}