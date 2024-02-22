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
            var playerEntity = _playersFilter.GetEntity(0);

            foreach (int idx in _triggeredEnemiesFilter)
            {
                var enemyEntity = _triggeredEnemiesFilter.GetEntity(idx);
                var enemyAttackRange = enemyEntity.Get<EnemyAttackRangeComponent>();
                ref var enemyDistanceToTargetComponent = ref enemyEntity.Get<DistanceToTargetComponent>().Value;
                
                if (enemyDistanceToTargetComponent < enemyAttackRange.Value)
                    enemyEntity.Get<EnemyAttackTargetComponent>().TargetEntity = playerEntity;

                else
                    enemyEntity.Del<EnemyAttackTargetComponent>();
            }
        }
    }
}