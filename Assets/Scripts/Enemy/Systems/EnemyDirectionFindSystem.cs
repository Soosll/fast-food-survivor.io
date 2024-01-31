using Enemy.Components;
using General.Components;
using Leopotam.Ecs;
using Player.Components.Main;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class EnemyDirectionFindSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag> _initEnemiesFilter;
        private EcsFilter<InitPlayerTag> _playerFilter;

        public void Run()
        {
            foreach (int idx in _initEnemiesFilter)
            {
                var enemyEntity = _initEnemiesFilter.GetEntity(idx);
                var enemyTransform = enemyEntity.Get<TransformLink>().Transform;
                
                var playerEntity = _playerFilter.GetEntity(0);
                var playerTransform = playerEntity.Get<TransformLink>().Transform;

                var direction = playerEntity.Get<TransformLink>().Transform.position - enemyEntity.Get<TransformLink>().Transform.position;
                
                enemyEntity.Get<MoveDirectionComponent>().Direction = direction;
            }
        }
    }
}