using General.Components.Parameters;
using Leopotam.Ecs;
using Projectiles.Components.Garlic;
using Projectiles.Components.General;
using UnityEngine;

namespace Projectiles.Systems.Garlic
{
    public class GarlicProjectileDurationSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<GarlicProjectileTag, DurationComponent> _garlicsDurationFilter;

        public void Run()
        {
            foreach (var idx in _garlicsDurationFilter)
            {
                var garlicEntity = _garlicsDurationFilter.GetEntity(idx);

               ref var durationComponent = ref garlicEntity.Get<DurationComponent>();
               
               durationComponent.Value -= Time.deltaTime;

               if (durationComponent.Value <= 0)
               {
                   garlicEntity.Del<DurationComponent>();
                   garlicEntity.Get<EndDurationTag>();
               }
            }
        }
    }
}