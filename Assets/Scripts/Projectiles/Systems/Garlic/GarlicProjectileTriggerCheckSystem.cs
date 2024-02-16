using Data.Static;
using Enemy.Components;
using General.Components;
using General.Components.Events;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Stats;
using Projectiles.Components.Garlic;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Projectiles.Systems.Garlic
{
    public class GarlicProjectileTriggerCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<GarlicProjectileTag> _projectilesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;
        
        public void Run()
        {
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            ref var damageStat = ref playerStatsEntity.Get<DamageStat>().ModifiedValue;
            
            foreach (int idx in _projectilesFilter)
            {
                var projectileEntity = _projectilesFilter.GetEntity(idx);

                ref var projectileEntityTransform = ref projectileEntity.Get<TransformLink>().Transform;

                ref var projectileCollider = ref projectileEntity.Get<BoxCollider2DLink>().BoxCollider2D;
                
                var hittedColliders = Physics2D.OverlapBoxAll(projectileEntityTransform.position, new Vector2(projectileCollider.size.x, projectileCollider.size.y), 90);
                
                if (hittedColliders.Length == 0)
                    continue;
                
                for (var i = 0; i < hittedColliders.Length; i++)
                {
                    if (hittedColliders[i].gameObject.TryGetComponent(out MonoEntity monoEntity))
                    {
                        if (!monoEntity.Entity.Has<InitEnemyTag>())
                            continue;
                
                        var enemyEntity = monoEntity.Entity;
                
                        if (enemyEntity.Has<GarlicHitBoxCooldownComponent>())
                            continue;

                        ref var hitByGarlicComponent = ref enemyEntity.Get<HitByGarlicComponent>();

                        if(enemyEntity.Has<IgnoreOverlapCheckCooldown>() && hitByGarlicComponent.LastHitEntity == projectileEntity)
                            continue;
                        
                        ref var ableHitsCount = ref projectileEntity.Get<GarlicProjectileAbleHitsComponent>().AbleHits;
                
                        ref var projectileDamage = ref projectileEntity.Get<DamageComponent>().Value;
                        ref var projectileKnockback = ref projectileEntity.Get<KnockbackComponent>().Value;
                        ref var projectileHitBox = ref projectileEntity.Get<HitBoxDelayComponent>().Value;
                
                        enemyEntity.Get<TakeDamageEvent>().Value += projectileDamage * damageStat;
                        enemyEntity.Get<KnockbackEvent>().Value = projectileKnockback;

                        ref var rotateAroundSpeedComponent = ref projectileEntity.Get<MoveSpeedAroundComponent>().Speed;
                        var fps = 1 / Time.deltaTime;
                        var distanceAngleInSecond = rotateAroundSpeedComponent * Time.deltaTime * StaticGameParameters.MoveAroundMultiplyer * fps;
                        var fullCircleTurnOverTime = 360 / distanceAngleInSecond;
                        var halfTurnOverTime = fullCircleTurnOverTime / 2;
                        
                        enemyEntity.Get<IgnoreOverlapCheckCooldown>().Value = halfTurnOverTime;
                        
                        hitByGarlicComponent.HitsCount++;
                        hitByGarlicComponent.LastHitEntity = projectileEntity;
                
                        if (hitByGarlicComponent.HitsCount >= ableHitsCount)
                            enemyEntity.Get<GarlicHitBoxCooldownComponent>().Value = projectileHitBox;
                    }
                }
            }
        }
    }
}