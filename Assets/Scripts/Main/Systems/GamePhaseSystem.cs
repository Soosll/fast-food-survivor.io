using System;
using Data.Enums;
using Data.RunTime;
using Data.Static;
using Extensions;
using Leopotam.Ecs;
using Mailbox;
using Player.Components;
using Player.Components.Abilities.Main;
using Player.Components.Experience;
using Player.Components.Spawn;
using Spawners.Components;
using UnityEngine;
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
                
                case GamePhase.SpellChoose:

                    Time.timeScale = 0;
                    _staticData.SceneDependencies.UIDependencies.AbilitiesPopupHolder.transform.Activate();

                    break;
                
                case GamePhase.AfterSpellChoose:
                    Time.timeScale = 1;
                    _staticData.SceneDependencies.UIDependencies.AbilitiesPopupHolder.transform.Diactivate();
                    _runTimeData.InGameData.GamePhase = GamePhase.GameLoop;
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
            _world.NewEntityWith<CreateSpawnersRequest>();

            _runTimeData.InGameData.GamePhase = GamePhase.InitWorld;
        }

        private void InitWorld()
        {
            _world.NewEntityWith<PlayerInitRequest>();
            _world.NewEntityWith<SpawnersDistributeRequest>();

            _runTimeData.InGameData.GamePhase = GamePhase.GameLoop;
        }
    }
}