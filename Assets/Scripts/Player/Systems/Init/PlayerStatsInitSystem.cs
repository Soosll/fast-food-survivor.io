using Leopotam.Ecs;
using Player.Components.Stats;
using Zun010.LeoEcsExtensions;

namespace Player.Systems.Init
{
    public class PlayerStatsInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        public void Init()
        {
            var statsEntity = _world.NewEntityWith<PlayerStatsTag>();

            statsEntity.Get<AmountStat>();
            statsEntity.Get<AreaStat>().BaseValue = 1;
            statsEntity.Get<CooldownStat>().BaseValue = 1;
            statsEntity.Get<DurationStat>().BaseValue = 1;

            statsEntity.Get<DamageStat>().BaseValue = 1;
            statsEntity.Get<DamageStat>().ModifiedValue = 1;
            
            statsEntity.Get<GrowthStat>();
            statsEntity.Get<MightStat>().BaseValue = 1;
            
            statsEntity.Get<MoveSpeedStat>().BaseValue = 1;
            statsEntity.Get<MoveSpeedStat>().ModifiedValue = 1;
           
            statsEntity.Get<ProjectilesSpeedStat>().BaseValue = 1;
            statsEntity.Get<ProjectilesSpeedStat>().ModifiedValue = 1;

            statsEntity.Get<PlayerKcalStat>().BaseValue = 1;
            statsEntity.Get<PlayerKcalStat>().ModifiedValue = 1;

            statsEntity.Get<PlayerLuckStat>().BaseValue = 1;
            statsEntity.Get<PlayerLuckStat>().ModifiedValue = 1;
        }
    }
}