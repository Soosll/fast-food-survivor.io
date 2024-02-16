using Data.Static;
using Experience.Components;
using Leopotam.Ecs;
using Player.Components.Main;
using UnityEngine;
using Zun010.MonoLinks;

namespace Experience.Systems
{
    public class MoveExperienceToPlayerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<MoveToPlayerTag, ExperienceTag> _moveExperienceFilter; 

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);

            ref var playerTransform = ref playerEntity.Get<TransformLink>().Transform;

            var experienceData = _staticData.ExperienceData;
            
            foreach (int idx in _moveExperienceFilter)
            {
                var experienceEntity = _moveExperienceFilter.GetEntity(idx);

                ref var experienceTransform = ref experienceEntity.Get<TransformLink>().Transform;

                experienceTransform.position = Vector3.MoveTowards(experienceTransform.position, playerTransform.position, experienceData.MoveSpeedTo / 100);
            }
        }
    }
}