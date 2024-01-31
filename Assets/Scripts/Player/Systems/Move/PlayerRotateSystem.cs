using Data.RunTime;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Main;
using Player.Components.Move;
using UnityEngine;

namespace Player.Systems.Move
{
    public class PlayerRotateSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;

        private EcsFilter<InitPlayerTag, RotateEvent> _playersFilter;

        public void Run()
        {
            foreach (int idx in _playersFilter)
            {
                EcsEntity playerEntity = _playersFilter.GetEntity(idx);

                RotateEvent rotateEvent = _playersFilter.Get2(idx);

                PlayerReferenceLink referenceLink = playerEntity.Get<PlayerReferenceLink>();
                
                Flip(referenceLink.SpriteRenderers, rotateEvent.Flip);
            }
        }

        private void Flip(SpriteRenderer[] spriteRenderers, bool value)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.flipX = value;
        }

    }
}