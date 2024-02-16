using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using UnityEngine;

namespace General.Systems
{
    public class IgnoreOverlapCooldownTickSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<IgnoreOverlapCheckCooldown> _ignoreOverlapCooldownsFilter;

        public void Run()
        {
            foreach (int idx in _ignoreOverlapCooldownsFilter)
            {
                ref var cooldown = ref _ignoreOverlapCooldownsFilter.Get1(idx);

                cooldown.Value -= Time.deltaTime;
                
                if(cooldown.Value <= 0)
                    _ignoreOverlapCooldownsFilter.GetEntity(idx).Del<IgnoreOverlapCheckCooldown>();
            }
        }
    }
}