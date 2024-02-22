using Enemy.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class OutOfMapEnemiesDistributeSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<EnemySpawnerTag> _enemiesSpawnersFilter;
        private EcsFilter<InitEnemyTag, OutOfMapTag> _outOfMapEnemiesFilter;

        public void Run()
        {
            foreach (int idx in _outOfMapEnemiesFilter)
            {
                var enemyEntity = _outOfMapEnemiesFilter.GetEntity(idx);
                var enemyTransform = enemyEntity.Get<TransformLink>().Transform;

                var randomSpawnPoint = _enemiesSpawnersFilter.GetEntity(Random.Range(0, _enemiesSpawnersFilter.GetEntitiesCount()));
                var spawnPointTransform = randomSpawnPoint.Get<TransformLink>().Transform;

                enemyTransform.position = new Vector3(spawnPointTransform.position.x, spawnPointTransform.position.y, enemyTransform.position.z);
                
                enemyEntity.Del<OutOfMapTag>();
            }
        }
    }
}