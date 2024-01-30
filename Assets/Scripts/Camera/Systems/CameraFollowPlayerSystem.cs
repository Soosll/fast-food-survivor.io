using Camera.Components;
using Cinemachine;
using Data.Static;
using Leopotam.Ecs;
using Player.Components.Main;
using UnityEngine;
using Zun010.MonoLinks;

namespace Camera.Systems
{
    public class CameraFollowPlayerSystem : IEcsRunSystem
    {
        private EcsWorld _world; // TODO Потом можно вешать на каждую камеру MonoEntityTag и находить так камеры +-
        private StaticData _staticData;

        private EcsFilter<InitPlayerComponent, CameraFollowComponent> _playerFilter;

        public void Run()
        {
            foreach (int idx in _playerFilter)
            {
                EcsEntity playerEntity = _playerFilter.GetEntity(idx);

                Transform playerTransform = playerEntity.Get<TransformLink>().Transform;
            
                CinemachineVirtualCameraBase mainCamera = _staticData.SceneDependencies.MainCamera;

                mainCamera.Follow = playerTransform;   
            }
        }
    }
}