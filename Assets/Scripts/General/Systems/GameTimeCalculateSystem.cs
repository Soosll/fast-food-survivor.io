using Data.Enums;
using Data.RunTime;
using General.Components;
using General.Components.Events;
using Leopotam.Ecs;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace General.Systems
{
    public class GameTimeCalculateSystem : IEcsRunSystem
    {
        private const int SecondsInOneMinute = 60;
        private const int OneSecond = 1;
        
        private EcsWorld _world;
        private RunTimeData _runTimeData;

        private float _gameLoopTime;

        private float _secondsEventTimer;
        
        public void Run()
        {
            if(_runTimeData.InGameData.GamePhase != GamePhase.GameLoop)
                return;

            _gameLoopTime += Time.deltaTime;
            _secondsEventTimer += Time.deltaTime;
            
            _runTimeData.InGameData.CurrentGameSecond = (int)_gameLoopTime;

            if (_secondsEventTimer >= OneSecond)
            {
                _world.NewEntityWith<SecondsUpEvent>();
                _secondsEventTimer = 0;
            }
            
            if (_gameLoopTime >= SecondsInOneMinute)
            {
                _runTimeData.InGameData.CurrentGameMinute++;

                _gameLoopTime = 0;
            }
            
        }
    }
}