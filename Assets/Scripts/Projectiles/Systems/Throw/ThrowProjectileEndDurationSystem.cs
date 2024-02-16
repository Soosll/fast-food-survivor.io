using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;
using Projectiles.Components.Throw;

namespace Projectiles.Systems.Throw
{
    public class ThrowProjectileEndDurationSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<ThrowProjectileTag, EndDurationTag> _projectilesFilter;
        
        public void Run()
        {
            foreach (var idx in _projectilesFilter)
            {
                var entity = _projectilesFilter.GetEntity(idx);
             
                entity.Del<EndDurationTag>();
                entity.Del<ThrowProjectileTag>();
                entity.Get<DestroyObjectTag>();
            }
        }
    }
}