using Enemy.Components;
using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components;
using Player.Components.Main;
using UnityEngine;
using Zun010.MonoLinks;

namespace Player.Systems.Find
{
    public class PlayerClosestEnemyFindSystem : IEcsRunSystem
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

            EcsEntity closestEntity = EcsEntity.Null;
            
            Vector3 closestEnemyPosition = Vector3.zero;
            Vector3 direction = Vector3.zero;
            
            float closestDistanceToEnemy = int.MaxValue;

            foreach (int idx in _enemiesFilter)
            {
                var enemyEntity = _enemiesFilter.GetEntity(idx);

                ref var enemyTransform = ref enemyEntity.Get<TransformLink>().Transform;

                var magnitude = (enemyTransform.position - playerTransform.position).magnitude;
                
                if (magnitude < closestDistanceToEnemy)
                {
                    closestEntity = enemyEntity;
                    closestEnemyPosition = enemyTransform.position;
                    closestDistanceToEnemy = magnitude;
                    direction = enemyTransform.position - playerTransform.position;
                }
            }

            playerEntity.Get<ClosestTargetPositionComponent>().Position = closestEnemyPosition;
            playerEntity.Get<ClosestTargetDirectionComponent>().Position = direction;

            if (closestEntity != EcsEntity.Null)
                playerEntity.Get<FoundTargetTag>();
            
            else
                playerEntity.Del<FoundTargetTag>();
        }
    }
}