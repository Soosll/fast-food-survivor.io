using Data.Static;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Spawners.Systems
{
    public class SpawnersCreateSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;

        private EcsFilter<CreateSpawnersRequest> _spawnRequestFilter;
        
        public void Run()
        {
            if(_spawnRequestFilter.GetEntitiesCount() == 0)
                return;
            
            _spawnRequestFilter.GetEntity(0).Destroy();

            SpawnersData spawnersData = _staticData.SceneDependencies.SpawnersData;

            Transform spawnersContainer = _staticData.SceneDependencies.SpawnersContainer;
            
            for (int i = 0; i < spawnersData.GeneralCount; i++)
            {
                var spawnerEntity = _world.NewEntityWith<EnemySpawnerTag>();
                var monoPrefab = Object.Instantiate(spawnersData.SpawnerMonoPrefab, spawnersContainer);
                
                monoPrefab.Make(ref spawnerEntity);
            }
        }
    }
}