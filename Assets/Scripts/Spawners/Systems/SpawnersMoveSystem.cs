using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Spawners.Components;
using UnityEngine;
using Zun010.MonoLinks;

namespace Spawners.Systems
{
    public class SpawnersMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;
        
        private EcsFilter<InitedSpawnPointTag> _distributedSpawnersFilter;

        private UnityEngine.Camera _camera;
        
        private Vector3 _cameraCenter;

        public void Init()
        {
            _camera = UnityEngine.Camera.main;

            if (_camera == null)
                Debug.LogWarning("Can`t find camera in scene");
        }

        public void Run()
        {
            _cameraCenter = _runTimeData.CameraParameters.WorldCenter;

            foreach (int idx in _distributedSpawnersFilter)
            {
                var spawnPointEntity = _distributedSpawnersFilter.GetEntity(idx);

                var spawnPointTransform = spawnPointEntity.Get<TransformLink>();

                var offset = spawnPointEntity.Get<SpawnerOffsetFromCameraComponent>();

                spawnPointTransform.Transform.position = _cameraCenter - offset.Value;
            }
        }
    }
}