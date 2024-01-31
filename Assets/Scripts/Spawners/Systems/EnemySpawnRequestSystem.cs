using Data.Enums;
using Data.Loaded;
using Data.RunTime;
using Data.Static;
using Data.Static.Enemies;
using General.Components;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Spawners.Systems
{
    public class EnemySpawnRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private EcsFilter<SecondsUpEvent> _secondsUpFilter;

        private EnemySpawnerData _spawnerData;
        
        public void Init()
        {
            _spawnerData = Resources.Load<EnemySpawnerData>("StaticData/SpawnEnemy/Level 1"); // TODO сделать лоад систему под это
        }

        public void Run()
        {
            if(_secondsUpFilter.GetEntitiesCount() == 0)
                return;
         
            _secondsUpFilter.GetEntity(0).Destroy();
            
            var currentSecond = _runTimeData.InGameData.CurrentGameSecond;
            var currentMinute = _runTimeData.InGameData.CurrentGameMinute;

            for (int i = 0; i < _spawnerData.EnemySpawnConfig.Count; i++)
            {
                var currentSpawnConfig = _spawnerData.EnemySpawnConfig[i];
                var spawnParameters = currentSpawnConfig.GetByMinute(currentMinute);
                
                if (spawnParameters.Length == 0)
                    continue;

                for (int j = 0; j < spawnParameters.Length; j++)
                {
                    var currentParameter = spawnParameters[j];

                    if(currentSecond == 0)
                        continue;
                    
                    if (currentSecond % currentParameter.SpawnRate == 0)
                    {
                        var entityRequest = _world.NewEntityWith<EnemySpawnRequest>();

                        entityRequest.Get<EnemySpawnRequest>().SpawnCount = currentParameter.SpawnCount;
                        entityRequest.Get<EnemySpawnRequest>().Id = currentSpawnConfig.EnemyId; 
                    }
                }
            }
        }
    }
}