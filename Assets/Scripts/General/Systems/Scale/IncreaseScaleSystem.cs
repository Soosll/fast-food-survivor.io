using DG.Tweening;
using General.Components.Scale;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems.Scale
{
    public class IncreaseScaleSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<IncreaseScaleRequest> _increaseObjectsFilter;

        public void Run()
        {
            foreach (int idx in _increaseObjectsFilter)
            {
                var entity = _increaseObjectsFilter.GetEntity(idx);

                var entityTransform = entity.Get<TransformLink>().Transform;

               ref var request = ref entity.Get<IncreaseScaleRequest>();

               entityTransform.DOScale(request.TargetScale, request.Time).OnComplete(() => SetIncreasedTag(entity));

               entity.Del<IncreaseScaleRequest>();
            }
        }

        private void SetIncreasedTag(EcsEntity entity)
        {
            entity.Get<IncreasedScaleTag>();
        }
    }
}