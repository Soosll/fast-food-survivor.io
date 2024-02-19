using Experience.Components;
using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Experience;
using Player.Components.Main;
using Projectiles.Components;
using Projectiles.Components.General;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Experience
{
    public class PlayerAccrueExperienceSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<AccureExperienceTag> _accrueExperienceFilter;

        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);
            
            foreach (int idx in _accrueExperienceFilter)
            {
                var experienceEntity = _accrueExperienceFilter.GetEntity(idx);

                ref var playerExperience = ref playerEntity.Get<PlayerExperienceComponent>().CurrentValue;

                ref var experienceToAccrue = ref experienceEntity.Get<TakeableExperienceComponent>().Value;

                playerExperience += experienceToAccrue;

                _world.NewEntityWith<PlayerExperienceIncreasedEvent>();
                
                experienceEntity.Del<AccureExperienceTag>();
                experienceEntity.Get<DestroyObjectTag>();
                experienceEntity.Get<DestroyEntityTag>().NeedToDelete = true;
            }
        }
        
    }
}