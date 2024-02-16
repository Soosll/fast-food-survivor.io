using Data.RunTime;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Main;
using Player.Components.Move;
using Player.Components.Stats;
using UnityEngine;

namespace Player.Systems.Move
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        public void Run()
        {
            var playerStatsEntity = _playerStatsFilter.GetEntity(0);

            var playerMoveSpeedStat = playerStatsEntity.Get<MoveSpeedStat>();
            
            foreach (int idx in _playersFilter)
            {
                EcsEntity playerEntity = _playersFilter.GetEntity(idx);

                var playerRigidbody = playerEntity.Get<Rigidbody2DLink>().Rigidbody2D;

                var playerDirection = playerEntity.Get<PlayerDirectionComponent>().Direction;

                var playerSpeed = playerEntity.Get<MoveSpeedComponent>().Value * playerMoveSpeedStat.ModifiedValue;

                playerRigidbody.velocity = playerDirection.normalized * playerSpeed;
            }
        }
    }
}