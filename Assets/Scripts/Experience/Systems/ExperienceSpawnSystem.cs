using Experience.Components;
using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Experience.Systems
{
    public class ExperienceSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<SpawnExperienceRequest> _spawnExperienceFilter;

        public void Run()
        {
            foreach (int idx in _spawnExperienceFilter)
            {
                var enemyEntity = _spawnExperienceFilter.GetEntity(idx);

                enemyEntity.Del<SpawnExperienceRequest>();
                
                ref var dropExperienceComponent = ref enemyEntity.Get<EnemyDropExperienceComponent>();

                ref var dropPoint = ref enemyEntity.Get<TransformLink>().Transform;

                var experienceComponent = _world.NewEntityWith<ExperienceTag>();
                experienceComponent.Get<TakeableExperienceComponent>().Value = dropExperienceComponent.Value;
                experienceComponent.Get<ExperienceOnGroundTag>();

                var experienceMonoPrefab = Object.Instantiate(dropExperienceComponent.Prefab, dropPoint.position, Quaternion.identity);
                
                experienceMonoPrefab.Make(ref experienceComponent);

                enemyEntity.Get<DestroyEntityTag>().NeedToDelete = true;
            }
        }
    }
}