using Data.RunTime;
using Leopotam.Ecs;
using UnityEngine;

namespace Camera.Systems
{
    public class CameraParametersCalculateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;

        private UnityEngine.Camera _camera;
        
        public void Init()
        {
            _camera = UnityEngine.Camera.main;


            _runTimeData.CameraParameters.WorldScale = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));
        }

        public void Run()
        {
            var cameraCenter = _camera.ScreenToWorldPoint(_camera.transform.position);
            _runTimeData.CameraParameters.WorldCenter = cameraCenter;
        }
    }
}