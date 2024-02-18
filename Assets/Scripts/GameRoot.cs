using Camera.Systems;
using Data.Loaded;
using Data.RunTime;
using Data.Static;
using Drop.Systems;
using Enemy.Systems;
using Experience.Systems;
using General.Systems;
using General.Systems.Scale;
using Leopotam.Ecs;
using Load.Systems;
using Main;
using Main.Systems;
using Player.Components.Abilities.Main;
using Player.Components.Experience;
using Player.Systems;
using Player.Systems.Abilities;
using Player.Systems.Abilities.Active;
using Player.Systems.Abilities.Active.Garlic;
using Player.Systems.Abilities.Active.OrangeSlingShot;
using Player.Systems.Abilities.Active.Weight;
using Player.Systems.Abilities.Init;
using Player.Systems.Abilities.Passive;
using Player.Systems.Abilities.Passive.Dumbbells;
using Player.Systems.Abilities.Passive.FastSneakers;
using Player.Systems.Abilities.Passive.VitaminB;
using Player.Systems.Abilities.Upgrade;
using Player.Systems.Experience;
using Player.Systems.Find;
using Player.Systems.Init;
using Player.Systems.Move;
using Player.Systems.Spawn;
using Projectiles.Systems;
using Projectiles.Systems.Fall;
using Projectiles.Systems.Garlic;
using Projectiles.Systems.Throw;
using Spawners.Systems;
using UI.Systems;
using UnityEngine;
using Zun010.LeoEcsExtensions;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private StaticData _staticData;

    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;

    private RunTimeData _runTimeData;
    private LoadedData _loadedData;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystems = new EcsSystems(_world);
            
        _updateSystems
            .Add(new ConvertGameObjectsToEntitiesSystem())
            .Add(new GamePhaseSystem())
            
            .Add(new LoadEnemiesDataSystem())
            .Add(new LoadEnemiesSpawnersDataSystem())
            .Add(new LoadAbilitiesDataSystem())
            .Add(new LoadAbilitiesPresetsDataSystem())
            .Add(new LoadLevelUpDataSystem())
            
            .Add(new StartGameSystem())
            .Add(new StartGamePanelPresenterSystem())
            .Add(new GameTimeCalculateSystem())
            .Add(new CooldownsTickSystem())
            .Add(new JoyStickPresenterSystem())
            .Add(new CameraParametersCalculateSystem())
            .Add(new RotateProjectileAroundPlayerSystem())
            .Add(new BlockRotateSystem())
            .Add(new IgnoreOverlapCooldownTickSystem())
            .Add(new DecreaseScaleSystem())
            .Add(new IncreaseScaleSystem())
            
            .Add(new PlayerInitSystem())
            .Add(new PlayerStatsInitSystem())
            .Add(new PlayerSpawnSystem())
            .Add(new PlayerJoystickDirectionSystem())
            .Add(new PlayerRotateSystem())
            .Add(new CameraFollowPlayerSystem())
            .Add(new PlayerStateMachineSystem())
            .Add(new PlayerClosestEnemyFindSystem())
            .Add(new PlayerDamageSystem())
            .Add(new PlayerExperienceFindSystem())
            .Add(new PlayerAccrueExperienceSystem())
            .Add(new PlayerLevelUpCheckSystem())
            .Add(new PlayerLevelUpSystem())
            
            .Add(new SpawnersCreateSystem())
            .Add(new SpawnersDistributeSystem())
            .Add(new SpawnersMoveSystem())
            
            .Add(new StandartObjectDestroySystem())
            
            .Add(new ThrowProjectileDurationTickSystem())
            .Add(new ProjectilePierceCounterSystem())
            
            .Add(new EnemySpawnSystem())
            .Add(new EnemySpawnRequestSystem())
            .Add(new DefaultEnemyInitSystem())
            .Add(new EnemyDirectionFindSystem())
            .Add(new EnemyAttackRangeCheckSystem())
            .Add(new EnemyKnockbackSystem())
            .Add(new EnemyDamageSetSystem())
            .Add(new FromEnemyDamageSystem())
            .Add(new EnemyDestroySystem())
            
            .Add(new ExperienceSpawnSystem())
            .Add(new MoveExperienceFromPlayerSystem())
            .Add(new MoveExperienceToPlayerSystem())
            .Add(new ExperienceTriggerCheckSystem())
            
            .Add(new ActiveAbilityInitSystem())
            .Add(new PassiveAbilityInitSystem())
            .Add(new ActiveAbilityUpgradeSystem())
            .Add(new PassiveAbilityUpgradeSystem())
            
            .Add(new RandomAbilityChooseSystem())
            .Add(new AbilityPopupShowSystem())
            .Add(new AbilityChooseButtonListenSystem())
            
            .Add(new CreateChosenAbilitySystem())
            .Add(new AfterSpellChooseContinueGameSystem())

            .Add(new OrangeSlingshotAbilitySystem())
            .Add(new GarlicAbilitySystem())
            .Add(new GarlicDurationSystem())
            .Add(new GarlicEndDurationSystem())
            .Add(new WeighAbilityBeginPhaseSystem())
            .Add(new WeightAbilityEndPhaseSystem())
            
            .Add(new VitaminBAbilitySystem())
            .Add(new DumbbellsAbilitySystem())
            .Add(new FastSneakersAbilitySystem())
            
            .Add(new ThrowProjectileTriggerCheckSystem())
            .Add(new ThrowProjectileEndDurationSystem())
            .Add(new GarlicHitBoxCooldownTickSystem())
            .Add(new GarlicProjectileTriggerCheckSystem())
            .Add(new FallProjectileTargetPointCheckSystem())
            .Add(new ReachedPointFallProjectileStopSystem())
            .Add(new FellProjectilesTriggerCheckSystem())
            
            .Add(new PlayerHealthObserveSystem())
            .Add(new PlayerReceivedDamageEventDeleteSystem())
            
            
            .Add(new DestroyDecreaseScaleProjectileSystem())
            .Add(new ProjectileDestroySystem())
            
            
            .Add(new EntityDestroySystem())
            ;

        _fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Add(new EnemyMoveSystem())
            .Add(new ProjectileMoveSystem())
            ;

        
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif

        _runTimeData = new RunTimeData();
        _loadedData = new LoadedData();
        
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
            .Inject(_loadedData)
            ;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _world.NewEntityWith<ChooseAbilityRequest>();
        }
        
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems.Run();
    }
}