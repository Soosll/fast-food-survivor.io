using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace Projectiles.Systems
{
    public class ProjectileDestroySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<ProjectileTag, DestroyObjectTag> _endDurationProjectilesFilter;

        public void Run()
        {
            foreach (int idx in _endDurationProjectilesFilter)
            {
                var entity = _endDurationProjectilesFilter.GetEntity(idx);

               ref var gameObject = ref entity.Get<GameObjectLink>().GameObject;

               Object.Destroy(gameObject);

               entity.Get<DestroyEntityTag>().NeedToDelete = true;
            }
        }
    }
}