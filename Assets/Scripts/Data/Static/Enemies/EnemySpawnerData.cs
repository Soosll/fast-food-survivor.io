using System;
using System.Collections.Generic;
using System.Linq;
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

        public EnemySpawnParameters[] GetByMinute(int minute) => 
            SpawnParameters.Where(s => s.From <= minute && s.To >= minute).ToArray();
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