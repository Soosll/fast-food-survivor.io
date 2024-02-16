using Data.Static;
using General.Components.Parameters;
using Leopotam.Ecs;
using Player.Components.Main;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems
{
    public class RotateProjectileAroundPlayerSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitPlayerTag, TransformLink> _playersFilter;
        private EcsFilter<ProjectileTag, MoveSpeedAroundComponent> _moveAroundObjectsFilter;
        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerTransform = _playersFilter.Get2(0);
            
            foreach (int idx in _moveAroundObjectsFilter)
            {
                var entity = _moveAroundObjectsFilter.GetEntity(idx);

                ref var entityTransform = ref entity.Get<TransformLink>().Transform;

                var moveAroundComponent = entity.Get<MoveSpeedAroundComponent>();

                entityTransform.RotateAround(playerTransform.Transform.position, Vector3.forward, moveAroundComponent.Speed * Time.deltaTime * StaticGameParameters.MoveAroundMultiplyer);
            }
        }
    }
}