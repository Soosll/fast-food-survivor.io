using Extensions;
using General.Components.Scale;
using Leopotam.Ecs;
using Projectiles.Components.Garlic;
using Projectiles.Components.General;
using UnityEngine;

namespace Projectiles.Systems.Garlic
{
    public class GarlicProjectileEndDurationSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<GarlicProjectileTag, EndDurationTag> _endDurationGarlicProjectilesFilter;

        public void Run()
        {
            foreach (int idx in _endDurationGarlicProjectilesFilter)
            {
                var entity = _endDurationGarlicProjectilesFilter.GetEntity(idx);
                
                entity.Del<EndDurationTag>();
                entity.Get<DecreaseScaleRequest>().TargetScale = new Vector3().MinimalObjectScale();
                entity.Get<DecreaseScaleRequest>().Time = 1.2f;
            }
        }
    }
}