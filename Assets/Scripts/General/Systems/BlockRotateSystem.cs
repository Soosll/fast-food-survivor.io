using General.Components;
using General.Components.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.MonoLinks;

namespace General.Systems
{
    public class BlockRotateSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<BlockRotateTag, TransformLink> _blockRotateObjectsFilter;

        public void Run()
        {
            foreach (int idx in _blockRotateObjectsFilter)
            {
                var transform = _blockRotateObjectsFilter.Get2(idx);

                transform.Transform.rotation = Quaternion.identity;
            }
        }
    }
}