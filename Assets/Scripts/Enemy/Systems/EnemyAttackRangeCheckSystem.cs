using Enemy.Components;
using Leopotam.Ecs;
using Player.Components.Main;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class EnemyAttackRangeCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag> _triggeredEnemiesFilter;
        private EcsFilter<InitPlayerTag> _playersFilter;
        
        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;
        
            var playerEntity = _playersFilter.GetEntity(0);

            foreach (int idx in _triggeredEnemiesFilter)
            {
                var enemyEntity = _triggeredEnemiesFilter.GetEntity(idx);
                var enemyTransform = enemyEntity.Get<TransformLink>().Transform;
                var enemyAttackRange = enemyEntity.Get<EnemyAttackRangeComponent>();
                
                var playerTransform = playerEntity.Get<TransformLink>().Transform;

                var distanceToPlayer = (playerTransform.position - enemyTransform.position).magnitude;

                if (distanceToPlayer < enemyAttackRange.Value)
                    enemyEntity.Get<EnemyAttackTargetComponent>().TargetEntity = playerEntity;

                else
                    enemyEntity.Del<EnemyAttackTargetComponent>();
            }
        }
    }
}