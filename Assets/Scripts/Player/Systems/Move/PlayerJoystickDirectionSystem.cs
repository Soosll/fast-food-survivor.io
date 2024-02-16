using Data.RunTime;
using Leopotam.Ecs;
using Player.Components.Main;
using Player.Components.Move;
using UnityEngine;

namespace Player.Systems.Move
{
    public class PlayerJoystickDirectionSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;
        
        private EcsFilter<InitPlayerTag> _playersFilter;

        public void Run()
        {
            foreach (int idx in _playersFilter)
            {
                EcsEntity playerEntity = _playersFilter.GetEntity(idx);

                Vector2 joyStickDirection = _runTimeData.InGameData.JoyStickDirection;

                playerEntity.Get<PlayerDirectionComponent>().Direction = joyStickDirection;
            }
        }
    }
}