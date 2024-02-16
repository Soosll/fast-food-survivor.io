using Leopotam.Ecs;
using Projectiles.Components;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems
{
    public class StandartObjectDestroySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<DestroyObjectTag> _destroyObjectsFilter;

        public void Run()
        {
            foreach (int idx in _destroyObjectsFilter)
            {
                var entity = _destroyObjectsFilter.GetEntity(idx);

                ref var gameObject = ref entity.Get<GameObjectLink>().GameObject;
                
                Object.Destroy(gameObject);
            }
        }
    }
}