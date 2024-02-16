using Experience.Components;
using General.Components;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Main;
using Projectiles.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Experience.Systems
{
    public class ExperienceTriggerCheckSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<TakeableExperienceComponent, OnTriggerEnter2DEvent> _takeAbleExperience;

        public void Run()
        {
            foreach (int idx in _takeAbleExperience)
            {
                var experienceEntity = _takeAbleExperience.GetEntity(idx);

                if (experienceEntity.TryGet(out OnTriggerEnter2DEvent trigger))
                {
                    
                    experienceEntity.Get<AccureExperienceTag>();
                    experienceEntity.Del<MoveToPlayerTag>();
                }
                
                experienceEntity.Del<OnTriggerEnter2DEvent>();
            }
        }
    }
}