using System.Linq;
using Data;
using Data.Loaded;
using Data.Static;
using Data.Static.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Load.Systems
{
    public class LoadEnemiesSpawnersDataSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private LoadedData _loadedData;
        
        public void Init()
        {
            _loadedData.EnemySpawnersLibrary.AllSpawnersData = Resources
                .LoadAll<EnemySpawnerData>(DataPath.EnemiesSpawnerData)
                .ToDictionary(x => x.name, x => x);
        }
    }
}