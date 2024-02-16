using Enemy.Components;
using General.Components.Events;
using General.Components.Parameters;
using Leopotam.Ecs;

namespace Enemy.Systems
{
    public class EnemyDamageSetSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag, EnemyAttackTargetComponent>.Exclude<CooldownComponent> _foundTargetEnemiesFilter;

        public void Run()
        {
            foreach (int idx in _foundTargetEnemiesFilter)
            {
                var enemyEntity = _foundTargetEnemiesFilter.GetEntity(idx);

                ref var enemyDamage = ref enemyEntity.Get<DamageComponent>().Value;

                ref var playerEntity = ref enemyEntity.Get<EnemyAttackTargetComponent>().TargetEntity;

                playerEntity.Get<TakeDamageEvent>().Value += enemyDamage;

                var attackCooldown = enemyEntity.Get<AttackCooldownComponent>().Value;

                enemyEntity.Get<CooldownComponent>().Value = attackCooldown;
            }
        }
    }
}