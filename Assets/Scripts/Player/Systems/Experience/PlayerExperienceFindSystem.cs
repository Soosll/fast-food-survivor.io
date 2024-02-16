using Data.Static;
using Experience.Components;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Experience;
using Player.Components.Main;
using Zun010.MonoLinks;

namespace Player.Systems.Experience
{
    public class PlayerExperienceFindSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private StaticData _staticData;
        
        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<TakeableExperienceComponent, ExperienceOnGroundTag> _takeableExperienceFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);
            
            foreach (var idx in _takeableExperienceFilter)
            {
                var experienceEntity = _takeableExperienceFilter.GetEntity(idx);

                var experienceData = _staticData.ExperienceData;
                
                ref var takeRadiusComponent = ref playerEntity.Get<TakeExperienceRangeComponent>().Range;

                ref var playerTransform = ref playerEntity.Get<TransformLink>().Transform;
                ref var experienceTransform = ref experienceEntity.Get<TransformLink>().Transform;

                var directionToExperience = experienceTransform.position - playerTransform.position;

                var sqrMagnitude = directionToExperience.sqrMagnitude;

                if (sqrMagnitude < takeRadiusComponent * takeRadiusComponent)
                {
                    experienceEntity.Get<MoveFromPlayerDistanceComponent>().TargetPoint = experienceTransform.position + directionToExperience.normalized * experienceData.MoveDistanceFrom;
                    experienceEntity.Del<ExperienceOnGroundTag>(); 
                }
            }
        }
    }
}