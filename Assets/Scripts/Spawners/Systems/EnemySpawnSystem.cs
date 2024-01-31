using Data.Loaded;
using Data.Static.Enemies;
using Enemy.Components;
using Extensions;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Spawners.Systems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private LoadedData _loadedData;
        
        private EcsFilter<EnemySpawnRequest> _spawnRequestsFilter;
        private EcsFilter<InitedSpawnPointTag> _initSpawnPointsFilter;

        public void Run()
        {
            if (_spawnRequestsFilter.GetEntitiesCount() == 0)
                return;

            for (int idx = 0; idx < _spawnRequestsFilter.GetEntitiesCount(); idx++)
            {
                var entity = _spawnRequestsFilter.GetEntity(idx);

                var spawnRequest = entity.Get<EnemySpawnRequest>();

                var enemyId = spawnRequest.Id;
                var monoPrefab = _loadedData.EnemiesLibrary.ForEnemy(enemyId).MonoPrefab;
                
                var spawnCount = spawnRequest.SpawnCount;

                for (int i = 0; i < spawnCount; i++)
                {
                    var enemyEntity = _world.NewEntityWith<EnemyIdComponent>();

                    enemyEntity.Get<EnemyIdComponent>().Id = spawnRequest.Id;
                    enemyEntity.Get<NotInitEnemyTag>();
                    
                    var randomNumber = Random.Range(0, _initSpawnPointsFilter.GetEntitiesCount() - 1);

                    var randomEntitySpawner = _initSpawnPointsFilter.GetEntity(randomNumber);

                    var randomSpawnPoint = randomEntitySpawner.Get<TransformLink>().Transform.position;

                    var randomOffsetFromPoint = new Vector3().Random(-3, 3); // TODO доделать отступ, придумать, куда его деть

                    var spawnPoint = new Vector3(randomSpawnPoint.x + randomOffsetFromPoint.x, randomSpawnPoint.y + randomOffsetFromPoint.y, 0);
                    
                    var monoEntity = Object.Instantiate(monoPrefab, spawnPoint, Quaternion.identity);
                    
                    monoEntity.Make(ref enemyEntity);
                }
                
                entity.Destroy();
            }
            
        }
    }
}