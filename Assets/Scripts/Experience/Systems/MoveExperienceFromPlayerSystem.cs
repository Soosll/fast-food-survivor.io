using Data.Static;
using Experience.Components;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.MonoLinks;

namespace Experience.Systems
{
    public class MoveExperienceFromPlayerSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;

        private EcsFilter<ExperienceTag, MoveFromPlayerDistanceComponent> _moveFromPlayerExperienceFilter;

        public void Run()
        {
            foreach (var idx in _moveFromPlayerExperienceFilter)
            {
                var experienceEntity = _moveFromPlayerExperienceFilter.GetEntity(idx);
                
                var experienceData = _staticData.ExperienceData;

                ref var experienceTransform = ref experienceEntity.Get<TransformLink>().Transform;
                ref var movePoint = ref experienceEntity.Get<MoveFromPlayerDistanceComponent>().TargetPoint;
                
                experienceTransform.position = Vector3.MoveTowards(experienceTransform.position, movePoint, experienceData.MoveSpeedFrom / 100);

                if (experienceTransform.position == movePoint)
                {
                    experienceEntity.Get<MoveToPlayerTag>();
                    experienceEntity.Del<MoveFromPlayerDistanceComponent>();
                }
            }
        }
    }
}