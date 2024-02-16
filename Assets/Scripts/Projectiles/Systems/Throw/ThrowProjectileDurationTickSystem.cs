using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;
using Projectiles.Components.Throw;
using UnityEngine;

namespace Projectiles.Systems.Throw
{
    public class ThrowProjectileDurationTickSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<ThrowProjectileTag, DurationComponent> _projectilesFilter;

        public void Run()
        {
            foreach (int idx in _projectilesFilter)
            {
                var projectileEntity = _projectilesFilter.GetEntity(idx);

                ref var projectileDuration = ref projectileEntity.Get<DurationComponent>().Value;

               projectileDuration -= Time.deltaTime;

               if (projectileDuration <= 0)
                   projectileEntity.Get<EndDurationTag>();
            }
        }
    }
}