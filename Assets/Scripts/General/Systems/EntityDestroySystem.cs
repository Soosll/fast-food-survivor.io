using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;

namespace General.Systems
{
    public class EntityDestroySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<DestroyEntityTag> _entitiesToDeleteFilter;

        public void Run()
        {
            foreach (int idx in _entitiesToDeleteFilter)
            {
                var entity = _entitiesToDeleteFilter.GetEntity(idx);
                
                if(entity.Get<DestroyEntityTag>().NeedToDelete)
                    entity.Destroy();
            }
        }
    }
}