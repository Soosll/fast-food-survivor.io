using Abilities.Components.Main;
using Camera.Components;
using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Experience;
using Player.Components.Main;
using Player.Components.Move;
using UnityEngine;
using Zun010.LeoEcsExtensions;

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

                playerEntity.Get<PlayerLevelComponent>().Level = 0;
                playerEntity.Get<MoveSpeedComponent>().Value = playerData.DefaultSpeed;
                playerEntity.Get<KcalComponent>().CurrentValue = playerData.DefaultKcal;
                playerEntity.Get<KcalComponent>().MaxValue = playerData.DefaultKcal;
                playerEntity.Get<TakeExperienceRangeComponent>().Range = playerData.DefaultTakeExperienceRange;
                playerEntity.Get<LuckComponent>().Value = playerData.DefaultLuck;
                playerEntity.Get<PlayerExperienceComponent>();

                playerEntity.Get<CameraFollowComponent>();

                var playerBaseAbilityId = playerData.PlayerBaseAbility.ToString();

                var initAbilityRequestEntity = _world.NewEntityWith<InitAbilityRequest>();

                initAbilityRequestEntity.Get<InitAbilityRequest>().Id = playerBaseAbilityId;
                
                playerEntity.Del<NotInitPlayerComponent>();
                playerEntity.Get<InitPlayerTag>();
            }
        }
    }
}