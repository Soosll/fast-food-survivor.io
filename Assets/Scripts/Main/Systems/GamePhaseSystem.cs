using System;
using Data.Enums;
using Data.RunTime;
using Data.Static;
using Extensions;
using Leopotam.Ecs;
using Mailbox;
using Player.Components;
using Player.Components.Spawn;
using Zun010.LeoEcsExtensions;

namespace Main.Systems
{
    public class GamePhaseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private MailboxEvent.Listener _gamePhaseChangeListener;

        private GamePhase _currentGamePhase;

        public void Run()
        {
            if (_currentGamePhase == _runTimeData.InGameData.GamePhase)
                return;

            _currentGamePhase = _runTimeData.InGameData.GamePhase;

            switch (_runTimeData.InGameData.GamePhase)
            {
                case GamePhase.NotStarted:
                    break;

                case GamePhase.CreateWorld:
                    CreateWorld();
                    break;

                case GamePhase.InitWorld:
                    InitWorld();
                    break;

                case GamePhase.GameLoop:
                    break;
                
                case GamePhase.End:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateWorld()
        {
            _staticData.SceneDependencies.UIDependencies.StartGamePanel.transform.Diactivate();
            _staticData.SceneDependencies.UIDependencies.PlayerHUD.transform.Activate();
            
            _world.NewEntityWith<PlayerSpawnRequest>();

            _runTimeData.InGameData.GamePhase = GamePhase.InitWorld;
        }

        private void InitWorld()
        {
            _world.NewEntityWith<PlayerInitRequest>();

            _runTimeData.InGameData.GamePhase = GamePhase.GameLoop;
        }
    }
}