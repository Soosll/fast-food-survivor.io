﻿using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.MonoLinks;

namespace Spawners.Systems
{
    public class SpawnersDistributeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;
        
        private EcsFilter<SpawnersDistributeRequest> _distributeRequestFilter;
        private EcsFilter<EnemySpawnerTag> _createdSpawnersFilter;

        private UnityEngine.Camera _camera;

        private Vector3 _cameraPosition;
        
        private int _distributedElements;

        public void Init() => 
            _camera = UnityEngine.Camera.main;

        public void Run()
        {
            if (_distributeRequestFilter.GetEntitiesCount() == 0)
                return;
            
            _distributeRequestFilter.GetEntity(0).Destroy();

            var spawnersData = _staticData.SceneDependencies.SpawnersData;

            var offsetFromCameraBound = spawnersData.OffsetFromCameraBounds;

            _cameraPosition = _camera.transform.position;

            var cameraWorldScale = _runTimeData.CameraParameters.WorldScale;
            
            DistributeSpawnersInXBound(spawnersData.TopCount, cameraWorldScale, offsetFromCameraBound);
            DistributeSpawnersInYBound(spawnersData.RightCount, cameraWorldScale, offsetFromCameraBound);
            DistributeSpawnersInXBound(spawnersData.BotCount, new Vector3(cameraWorldScale.x, -cameraWorldScale.y, cameraWorldScale.z), -offsetFromCameraBound);
            DistributeSpawnersInYBound(spawnersData.LeftCount, new Vector3(-cameraWorldScale.x, cameraWorldScale.y, cameraWorldScale.z), -offsetFromCameraBound);
        }

        private void DistributeSpawnersInXBound(int spawnersCount,Vector3 angularPoint, float offsetFromCameraBound)
        {
            if(spawnersCount == 0)
                return;
            
            var distanceBetweenSpawners = angularPoint.x * 2 / (spawnersCount - 1);

            for (int i = 0; i < spawnersCount; i++)
            {
                float currentDistance = i * distanceBetweenSpawners;

                var entity = _createdSpawnersFilter.GetEntity(_distributedElements);
                var entityTransform = entity.Get<TransformLink>();
                
                entity.Get<InitedSpawnPointTag>();
                _distributedElements++;
                
                entityTransform.Transform.position = angularPoint + new Vector3(0, offsetFromCameraBound, 0) - new Vector3(currentDistance, 0, 0);

                var offsetFromCameraCenter = _cameraPosition - entityTransform.Transform.position;

                entity.Get<SpawnerOffsetFromCameraComponent>().Value = offsetFromCameraCenter;
            }
        }

        private void DistributeSpawnersInYBound(int spawnersCount,Vector3 angularPoint, float offsetFromCameraBound)
        {
            if(spawnersCount == 0)
                return;
            
            var distanceBetweenSpawners = angularPoint.y * 2 / (spawnersCount - 1);

            for (int i = 0; i < spawnersCount; i++)
            {
                float currentDistance = i * distanceBetweenSpawners;

                var entity = _createdSpawnersFilter.GetEntity(_distributedElements);
                var entityTransform = entity.Get<TransformLink>();
                
                entity.Get<InitedSpawnPointTag>();

                _distributedElements++;
                
                entityTransform.Transform.position = angularPoint + new Vector3(offsetFromCameraBound, 0, 0) - new Vector3(0, currentDistance, 0);
                
                var offsetFromCameraCenter = _cameraPosition - entityTransform.Transform.position;

                entity.Get<SpawnerOffsetFromCameraComponent>().Value = offsetFromCameraCenter;
            }
        }
    }
}