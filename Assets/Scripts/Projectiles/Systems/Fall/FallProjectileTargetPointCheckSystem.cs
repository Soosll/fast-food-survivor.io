using General.Components.Parameters;
using Leopotam.Ecs;
using Projectiles.Components.Fall;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace Projectiles.Systems.Fall
{
    public class FallProjectileTargetPointCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<FallProjectileTag> _fallProjectilesFilter;

        public void Run()
        {
            foreach (int idx in _fallProjectilesFilter)
            {
                var projectileEntity = _fallProjectilesFilter.GetEntity(idx);

                ref var fallProjectileComponent = ref projectileEntity.Get<FallProjectileComponent>();

                ref var projectileEntityTransform = ref projectileEntity.Get<TransformLink>().Transform;

                var fallDirection = fallProjectileComponent.TargetFallPoint - projectileEntityTransform.position;
                
                if (fallDirection.magnitude <= 0.3f)
                {
                    projectileEntity.Del<FallProjectileTag>();
                    projectileEntity.Get<ReachedFallPointTag>();
                }
            }
        }
    }
}