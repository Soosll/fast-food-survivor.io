using Extensions;
using General.Components;
using General.Components.Scale;
using General.Components.Tags;
using Leopotam.Ecs;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems.Scale
{
    public class DestroyDecreaseScaleProjectileSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<ProjectileTag, DecreasedScaleTag> _decreasedProjectilesScaleFilter;

        public void Run()
        {
            foreach (int idx in _decreasedProjectilesScaleFilter)
            {
                var entity = _decreasedProjectilesScaleFilter.GetEntity(idx);

                ref var entityTransform = ref entity.Get<TransformLink>().Transform;

                if (entityTransform.localScale == new Vector3().MinimalObjectScale())
                {
                    entity.Get<DestroyObjectTag>();
                    entity.Get<DestroyEntityTag>().NeedToDelete = true;
                }
            }
        }
    }
}