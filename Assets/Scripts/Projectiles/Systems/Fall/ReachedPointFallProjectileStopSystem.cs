using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Projectiles.Components.Fall;
using UnityEngine;

namespace Projectiles.Systems.Fall
{
    public class ReachedPointFallProjectileStopSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<ReachedFallPointTag> _fellProjectilesFilter;
        
        public void Run()
        {
            foreach (int idx in _fellProjectilesFilter)
            {
                var projectileEntity = _fellProjectilesFilter.GetEntity(idx);

                projectileEntity.Del<MoveComponent>();
                projectileEntity.Del<MoveDirectionComponent>();
                projectileEntity.Del<ReachedFallPointTag>();
                
                projectileEntity.Get<FellProjectileTag>();
                
                ref var projectileRigidbody2D = ref projectileEntity.Get<Rigidbody2DLink>().Rigidbody2D;
                projectileRigidbody2D.velocity = Vector2.zero;
            }
        }
    }
}