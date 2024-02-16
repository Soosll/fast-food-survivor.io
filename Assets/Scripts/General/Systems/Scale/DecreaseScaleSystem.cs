using DG.Tweening;
using General.Components.Scale;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems.Scale
{
    public class DecreaseScaleSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<DecreaseScaleRequest> _decreaseObjectsFilter;

        public void Run()
        {
            foreach (int idx in _decreaseObjectsFilter)
            {
                var entity = _decreaseObjectsFilter.GetEntity(idx);

                var entityTransform = entity.Get<TransformLink>().Transform;

                ref var request = ref entity.Get<DecreaseScaleRequest>();

                if (request.Delay == 0)
                    entityTransform.DOScale(request.TargetScale, request.Time).SetEase(Ease.Linear)
                        .OnComplete(() => SetDecreasedScaleTag(entity));

                else
                    entityTransform.DOScale(request.TargetScale, request.Time).SetDelay(request.Delay).SetEase(Ease.Linear)
                        .OnComplete(() => SetDecreasedScaleTag(entity));

                entity.Del<DecreaseScaleRequest>();
            }
        }

        private void SetDecreasedScaleTag(EcsEntity entity) =>
            entity.Get<DecreasedScaleTag>();
    }
}