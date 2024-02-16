using Enemy.Components;
using General.Components;
using General.Components.Events;
using General.Components.Parameters;
using Leopotam.Ecs;
using Player.Components.Stats;
using Projectiles.Components;
using Projectiles.Components.General;
using Projectiles.Components.Throw;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Projectiles.Systems.Throw
{
    public class ThrowProjectileTriggerCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<ProjectileTag, OnTriggerEnter2DEvent> _projectilesFilter;

        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        public void Run()
        {
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            ref var damageStat = ref playerStatsEntity.Get<DamageStat>().ModifiedValue;
            
            foreach (int idx in _projectilesFilter)
            {
                var projectileEntity = _projectilesFilter.GetEntity(idx);
                ref var projectileDamage = ref projectileEntity.Get<DamageComponent>().Value;
                ref var projectileKnockback = ref projectileEntity.Get<KnockbackComponent>().Value;
                
                if (projectileEntity.TryGet(out OnTriggerEnter2DEvent trigger))
                {
                    var enemyEntity = trigger.GetOtherEntity();
                    
                    enemyEntity.Get<TakeDamageEvent>().Value += projectileDamage * damageStat;
                    enemyEntity.Get<KnockbackEvent>().Value = projectileKnockback;
                    projectileEntity.Get<TriggerEnemyEvent>().TriggersCount++;
                }
                
                projectileEntity.Del<OnTriggerEnter2DEvent>();
            }
        }
    }
}