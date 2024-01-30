using Data.RunTime;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Main;
using Player.Components.Move;
using UnityEngine;

namespace Player.Systems.Move
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<InitPlayerComponent> _playersFilter;

        public void Run()
        {
            foreach (int idx in _playersFilter)
            {
                EcsEntity playerEntity = _playersFilter.GetEntity(idx);

                var playerRigidbody = playerEntity.Get<Rigidbody2DLink>().Rigidbody2D;

                var playerDirection = playerEntity.Get<PlayerDirectionComponent>().Direction;

                var playerSpeed = playerEntity.Get<MoveSpeedStat>().Value;

                playerRigidbody.velocity = playerDirection.normalized * playerSpeed;
            }
        }
    }
}