using System.Collections.Generic;
using Data.Static.Enemies;

namespace Data.Loaded
{
    public class EnemiesLibrary
    {
        public Dictionary<string, EnemyData> AllEnemies { get; set; } = new();

        public EnemyData ForEnemy(string id) => 
            AllEnemies.TryGetValue(id, out EnemyData enemyData) ? enemyData : null;
    }
}