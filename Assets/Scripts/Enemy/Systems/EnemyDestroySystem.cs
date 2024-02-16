using Enemy.Components;
using Experience.Components;
using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace Enemy.Systems
{
    public class EnemyDestroySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag, DestroyObjectTag> _enemiesToDestroyFilter;

        public void Run()
        {
            foreach (int idx in _enemiesToDestroyFilter)
            {
                var enemyEntity = _enemiesToDestroyFilter.GetEntity(idx);

                ref var enemyEntityGameObject = ref enemyEntity.Get<GameObjectLink>().GameObject;
                
                Object.Destroy(enemyEntityGameObject);

                enemyEntity.Get<DestroyEntityTag>();
                enemyEntity.Get<SpawnExperienceRequest>();
                enemyEntity.Del<InitEnemyTag>();
            }
        }
    }
}