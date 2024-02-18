using Data.Enums;
using Enemy.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components;
using Player.Components.Abilities.Main;
using Player.Components.Main;
using Projectiles.Components.Fall;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Player.Systems.Abilities.Active.Weight
{
    public class WeighAbilityBeginPhaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitAbilityTag>.Exclude<CooldownComponent> _initAbilitiesFilter;
        private EcsFilter<InitPlayerTag, FoundTargetTag> _playersFilter;
        private EcsFilter<InitEnemyTag> _enemiesFilter;

        private string _abilityId;
        
        public void Init()
        {
            _abilityId = AbilitiesId.Weight.ToString();
        }

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            foreach (int idx in _initAbilitiesFilter)
            {
                var randomEnemyEntity = _enemiesFilter.GetEntity(Random.Range(0, _enemiesFilter.GetEntitiesCount()));
                
                
                var abilityEntity = _initAbilitiesFilter.GetEntity(idx);

                ref var abilityComponent = ref abilityEntity.Get<PlayerActiveAbilityComponent>();
                
                if(abilityComponent.Id != _abilityId)
                    continue;
                
                for (int i = 0; i < abilityComponent.Amount; i++)
                {
                    var projectileEntity = _world.NewEntityWith<ProjectileTag>();
                    
                    projectileEntity.Get<ProjectileTag>().Id = abilityComponent.Id;
                    projectileEntity.Get<FallProjectileTag>();
                    projectileEntity.Get<MoveComponent>().Value = abilityComponent.Speed;
                    projectileEntity.Get<MoveDirectionComponent>().Direction = Vector3.down;
                    projectileEntity.Get<DamageComponent>().Value = abilityComponent.Damage;
                    projectileEntity.Get<KnockbackComponent>().Value = abilityComponent.Knockback;

                    ref var projectilePrefab = ref abilityComponent.ProjectilePrefab;

                    ref var fallProjectileComponent = ref projectileEntity.Get<FallProjectileComponent>();

                    ref var randomEnemyTransform = ref randomEnemyEntity.Get<TransformLink>().Transform;

                    fallProjectileComponent.TargetFallPoint = randomEnemyTransform.position;
                    fallProjectileComponent.BeginFallPoint = randomEnemyTransform.position + new Vector3(0, 15, 0);
                    
                   var monoPrefab = Object.Instantiate(projectilePrefab, fallProjectileComponent.BeginFallPoint, Quaternion.identity);

                   monoPrefab.Make(ref projectileEntity);
                   
                   ref var projectileShadowReference = ref projectileEntity.Get<SpriteRendererReferenceLink>().SpriteRenderer;
                   
                   projectileShadowReference.transform.SetParent(null);
                   projectileShadowReference.transform.position = fallProjectileComponent.TargetFallPoint;
                   
                   ref var projectileCollider = ref projectileEntity.Get<BoxCollider2DLink>().BoxCollider2D;

                   projectileCollider.enabled = false;

                   abilityEntity.Get<CooldownComponent>().Value = abilityComponent.Cooldown;
                }
            }
        }
    }
}