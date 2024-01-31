using Enemy.Components;
using General.Components;
using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;

namespace Enemy.Systems
{
    public class EnemyMoveSystem : IEcsRunSystem // TODO добавить стейт машину и мув тег
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag> _initEnemiesFilter;

        public void Run()
        {
            foreach (int idx in _initEnemiesFilter)
            {
                var enemyEntity = _initEnemiesFilter.GetEntity(idx);

                var enemyRigidbody = enemyEntity.Get<Rigidbody2DLink>().Rigidbody2D;
                var enemyDirection = enemyEntity.Get<MoveDirectionComponent>().Direction;
                var enemySpeed = enemyEntity.Get<MoveComponent>().Speed;

                enemyRigidbody.velocity = enemyDirection.normalized * enemySpeed;
            }
        }
    }
}