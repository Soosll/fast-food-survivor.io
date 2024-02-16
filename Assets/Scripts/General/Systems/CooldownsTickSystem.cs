using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using UnityEngine;

namespace General.Systems
{
    public class CooldownsTickSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<CooldownComponent> _cooldownsFilter;

        public void Run()
        {
            foreach (var idx in _cooldownsFilter)
            {
               var entity = _cooldownsFilter.GetEntity(idx);

              ref var cooldownComponent = ref entity.Get<CooldownComponent>();

               cooldownComponent.Value -= Time.deltaTime;
               
               if(cooldownComponent.Value <= 0)
                   entity.Del<CooldownComponent>();
            }
        }
    }
}