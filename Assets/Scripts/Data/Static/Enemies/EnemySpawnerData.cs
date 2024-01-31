using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Static.Enemies
{
    [CreateAssetMenu(menuName = "StaticData/EnemySpawner", fileName = "LevelName", order = 51)]
    public class EnemySpawnerData : ScriptableObject
    {
        public List<EnemySpawnConfig> EnemySpawnConfig;
    }

    [Serializable]
    public class EnemySpawnConfig
    {
        public string EnemyId;
        
        public List<EnemySpawnParameters> SpawnParameters;
    }

    [Serializable]
    public class EnemySpawnParameters
    {
        public int From;
        public int To;
        public int SpawnCount;
        public int SpawnRate;
    }
}