using Enemy.Components;
using General.Components.Events;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Stats;
using Projectiles.Components.Fall;
using UnityEngine;
using Zun010.MonoLinks;

namespace Projectiles.Systems.Fall
{
    public class FellProjectilesTriggerCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<PlayerStatsTag> _playerStatsFilter;
        private EcsFilter<FellProjectileTag> _fellProjectilesFilter;

        public void Run()
        {
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            ref var damageStats = ref playerStatsEntity.Get<DamageStat>();
            
            foreach (int idx in _fellProjectilesFilter)
            {
                var projectileEntity = _fellProjectilesFilter.GetEntity(idx);

                ref var projectileTransform = ref projectileEntity.Get<TransformLink>().Transform;
                ref var projectileBoxCollider = ref projectileEntity.Get<BoxCollider2DLink>().BoxCollider2D;

                var hitColliders = Physics2D.OverlapBoxAll(projectileBoxCollider.transform.localPosition, projectileBoxCollider.size, 90);

                if (hitColliders.Length == 0)
                    continue;

                ref var projectileDamage = ref projectileEntity.Get<DamageComponent>().Value;
                ref var projectileKnockback = ref projectileEntity.Get<KnockbackComponent>().Value;
                
                for (var i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].TryGetComponent(out MonoEntity monoEntity))
                    {
                        if (!monoEntity.Entity.Has<InitEnemyTag>())
                            continue;

                        var enemyEntity = monoEntity.Entity;

                        enemyEntity.Get<TakeDamageEvent>().Value = projectileDamage + projectileDamage * damageStats.ModifiedValue;
                        enemyEntity.Get<KnockbackEvent>().Value = projectileKnockback;
                    }
                }
                
                projectileEntity.Del<FellProjectileTag>();
                projectileEntity.Get<FallenProjectileTag>();
            }
        }
    }
}