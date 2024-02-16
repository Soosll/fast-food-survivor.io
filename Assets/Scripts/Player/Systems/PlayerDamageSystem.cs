using Enemy.Components;
using General.Components.Events;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Stats;
using Projectiles.Components.General;

namespace Player.Systems
{
    public class PlayerDamageSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag, TakeDamageEvent> _takeDamageEntitiesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        public void Run()
        {
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            ref var damageStat = ref playerStatsEntity.Get<DamageStat>().ModifiedValue;
            
            foreach (int idx in _takeDamageEntitiesFilter)
            {
                var enemyEntity = _takeDamageEntitiesFilter.GetEntity(idx);
                
                var damageEvent = _takeDamageEntitiesFilter.Get2(idx);
                
                ref var enemyHealth = ref enemyEntity.Get<KcalComponent>().CurrentValue;
                ref var damage = ref damageEvent.Value;

                enemyHealth -= damage * damageStat;
                
                if (enemyHealth <= 0)
                    enemyEntity.Get<DestroyObjectTag>();

                enemyEntity.Del<TakeDamageEvent>();
            }
        }
    }
}