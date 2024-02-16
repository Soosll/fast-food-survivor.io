using System.Linq;
using Data;
using Data.Loaded;
using Data.Static;
using Data.Static.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Load.Systems
{
    public class LoadEnemiesDataSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;
        
        public void Init()
        {
            _loadedData.EnemiesLibrary.AllEnemies = Resources
                .LoadAll<EnemyData>(DataPath.EnemiesDataPath)
                .ToDictionary(x => x.Id, x => x);
        }
    }
}