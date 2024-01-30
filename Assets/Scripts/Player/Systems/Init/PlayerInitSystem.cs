using Camera.Components;
using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Main;
using Player.Components.Move;

namespace Player.Systems.Init
{
    public class PlayerInitSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<PlayerInitRequest> _initFilter;
        private EcsFilter<NotInitPlayerComponent> _preInitPlayersFilter;

        private RunTimeData _runTimeData;

        public void Run()
        {
            if(_initFilter.GetEntitiesCount() == 0)
                return;
            
            _initFilter.GetEntity(0).Destroy();

            foreach (int idx in _preInitPlayersFilter)
            {
                EcsEntity playerEntity = _preInitPlayersFilter.GetEntity(idx);

                PlayerData playerData = _runTimeData.InGameData.PlayerData;
                
                playerEntity.Get<MoveSpeedStat>().Value = playerData.DefaultSpeed;
                playerEntity.Get<DamageComponent>().Damage = playerData.DefaultDamage;
                playerEntity.Get<KcalComponent>().Value = playerData.DefaultKcal;
                
                playerEntity.Get<CameraFollowComponent>();
                
                playerEntity.Del<NotInitPlayerComponent>();
                playerEntity.Get<InitPlayerComponent>();
            }
        }
    }
}