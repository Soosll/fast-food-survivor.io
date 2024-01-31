using System;
using Camera.Systems;
using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Main.Systems;
using Player.Systems;
using Player.Systems.Init;
using Player.Systems.Move;
using Player.Systems.Spawn;
using Spawners.Systems;
using UI.Systems;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private StaticData _staticData;

    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;

    private RunTimeData _runTimeData;
    
    private void Awake()
    {
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystems = new EcsSystems(_world);
            
        _updateSystems
            .Add(new ConvertGameObjectsToEntitiesSystem())
            .Add(new GamePhaseSystem())
            .Add(new StartGameSystem())
            .Add(new StartGamePanelPresenterSystem())
            .Add(new JoyStickPresenterSystem())
            .Add(new CameraParametersCalculateSystem())
            .Add(new PlayerInitSystem())
            .Add(new PlayerSpawnSystem())
            .Add(new PlayerJoystickDirectionSystem())
            .Add(new PlayerRotateSystem())
            .Add(new CameraFollowPlayerSystem())
            .Add(new PlayerStateMachineSystem())
            
            .Add(new SpawnersCreateSystem())
            .Add(new SpawnersDistributeSystem())
            .Add(new SpawnersMoveSystem())
            ;

        _fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            ;

        
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif

        _runTimeData = new RunTimeData();

        InjectUpdateSystems();

        InjectFixedUpdateSystems();
        
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    private void InjectFixedUpdateSystems()
    {
        _fixedUpdateSystems
            .Inject(_staticData)
            .Inject(_runTimeData)
            ;
    }

    private void InjectUpdateSystems()
    {
        _updateSystems
            .Inject(_staticData)
            .Inject(_runTimeData)
            ;
    }

    private void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems.Run();
    }
}