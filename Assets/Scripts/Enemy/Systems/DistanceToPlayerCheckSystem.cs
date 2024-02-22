using Enemy.Components;
using Leopotam.Ecs;
using Player.Components.Main;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class DistanceToPlayerCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<InitEnemyTag> _enemiesFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);
            ref var playerTransform = ref playerEntity.Get<TransformLink>().Transform;
            
            foreach (int idx in _enemiesFilter)
            {
                var enemyEntity = _enemiesFilter.GetEntity(idx);
                ref var enemyTransform = ref enemyEntity.Get<TransformLink>().Transform;
                ref var enemyDistanceToTargetValue = ref enemyEntity.Get<DistanceToTargetComponent>().Value;

                enemyDistanceToTargetValue = (enemyTransform.position - playerTransform.position).magnitude;
            }
        }
    }
}