using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using UI.Input;

namespace UI.Systems
{
    public class JoyStickPresenterSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RunTimeData _runTimeData;
        private StaticData _staticData;
        
        public void Run()
        {
            JoyStick joyStick = _staticData.SceneDependencies.UIDependencies.PlayerHUD.JoyStick;

            _runTimeData.InGameData.JoyStickDirection = joyStick.Direction;
        }
    }
}