using Leopotam.Ecs;
using UnityEngine;
using Zun010.MonoLinks;

namespace Main.Systems
{
    public class ConvertGameObjectsToEntitiesSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        public void Init()
        {
            var monoEntities = Object.FindObjectsOfType<MonoEntity>();

            foreach (MonoEntity monoEntity in monoEntities)
            {
                var newEntity = _world.NewEntity();
                
                monoEntity.Make(ref newEntity);
            }
        }
    }
}