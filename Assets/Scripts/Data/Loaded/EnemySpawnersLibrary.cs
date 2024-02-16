using System.Collections.Generic;
using Data.Static.Enemies;

namespace Data.Loaded
{
    public class EnemySpawnersLibrary
    {
        public Dictionary<string, EnemySpawnerData> AllSpawnersData { get; set; } = new();

        public EnemySpawnerData ForSpawner(string id) => 
            AllSpawnersData.TryGetValue(id, out EnemySpawnerData data) ? data : null;
    }
}