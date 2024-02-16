using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Player.Components.Main;
using Player.Components.Spawn;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Player.Systems.Spawn
{
    public class PlayerSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private EcsFilter<PlayerSpawnRequest> _playerSpawnRequestFilter;
        private EcsFilter<PlayerSpawnPointTag> _spawnPointsFilter;

        public void Run()
        {
            if (_playerSpawnRequestFilter.GetEntitiesCount() == 0)
                return;

            _playerSpawnRequestFilter.GetEntity(0).Destroy();
            
            foreach (int idx in _spawnPointsFilter)
            {
                EcsEntity playerEntity = _world.NewEntityWith<NotInitPlayerComponent>();
                
                string currentPlayerId = _runTimeData.InGameData.ChosenPlayerId;
                PlayerData playerData = _staticData.PlayersLibrary.GetDataById(currentPlayerId);

                _runTimeData.InGameData.PlayerData = playerData;
                
                EcsEntity spawnPointEntity = _spawnPointsFilter.GetEntity(idx);
                Transform spawnPoint = spawnPointEntity.Get<TransformLink>().Transform;

                MonoEntity monoEntityPrefab = playerData.Prefab;
                MonoEntity monoEntity = Object.Instantiate(monoEntityPrefab, spawnPoint.position, Quaternion.identity);
                
                monoEntity.Make(ref playerEntity);
            }
        }
    }
}