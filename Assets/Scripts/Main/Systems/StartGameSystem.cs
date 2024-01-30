using Data.Enums;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using UnityEngine;

namespace Main.Systems
{
    public class StartGameSystem : IEcsRunSystem // TODO сделать действительно началом игры +-
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;
        
        private EcsFilter<StartGameRequest> _startGameFilter;

        public void Run()
        {
            if(_startGameFilter.GetEntitiesCount() == 0)
                return;
            
            _startGameFilter.GetEntity(0).Destroy();

            _runTimeData.InGameData.GamePhase = GamePhase.CreateWorld;
        }
    }
}