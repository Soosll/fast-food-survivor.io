using Data.RunTime;
using Data.Static;
using Enemy.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using Player.Components.Main;
using UnityEngine;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class EnemiesOutOfMapCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;
        private RunTimeData _runTimeData;
        
        private EcsFilter<InitEnemyTag> _enemiesFilter;
        private EcsFilter<InitPlayerTag> _playersFilter;

        public void Run()
        {
            var mapBorders = _staticData.SceneDependencies.MapBorders;
            var cameraWorldScale = _runTimeData.CameraParameters.WorldScale;
            var cameraPosition = _runTimeData.CameraParameters.CameraPosition;
            
            foreach (int idx in _enemiesFilter)
            {
                var enemyEntity = _enemiesFilter.GetEntity(idx);
                ref var enemyTransform = ref enemyEntity.Get<TransformLink>().Transform;

                var direction = cameraPosition - enemyTransform.position;
                
                var xBorderWithCamera = mapBorders.BorderX + cameraWorldScale.x;
                var yBorderWithCamera = mapBorders.BorderY + cameraWorldScale.y;

                if (Mathf.Abs(direction.x) > xBorderWithCamera || Mathf.Abs(direction.y) > yBorderWithCamera)
                    enemyEntity.Get<OutOfMapTag>();
            }
        }
    }
}