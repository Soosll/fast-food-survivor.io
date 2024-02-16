using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;

namespace Projectiles.Systems
{
    public class ProjectilePierceCounterSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<ProjectileTag, TriggerEnemyEvent, PierceComponent> _projectilesFilter;

        public void Run()
        {
            foreach (int idx in _projectilesFilter)
            {
                var projectileEntity = _projectilesFilter.GetEntity(idx);

                var triggerEvent = _projectilesFilter.Get2(idx);
                
                ref var projectilePierce = ref projectileEntity.Get<PierceComponent>().Value;

                projectilePierce -= triggerEvent.TriggersCount;

                projectileEntity.Del<TriggerEnemyEvent>();

                if (projectilePierce <= 0)
                    projectileEntity.Get<DestroyObjectTag>();
            }
        }
    }
}