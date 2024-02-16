using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Stats;
using Projectiles.Components.General;

namespace Projectiles.Systems
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<ProjectileTag, MoveComponent> _projectilesFilter;

        private EcsFilter<PlayerStatsTag> _statsFilter;

        public void Run()
        {
            var statsEntity = _statsFilter.GetEntity(0);

            foreach (int idx in _projectilesFilter)
            {
                var projectileEntity = _projectilesFilter.GetEntity(idx);

                var projectileRigidbody = projectileEntity.Get<Rigidbody2DLink>().Rigidbody2D;

                var moveSpeedStat = statsEntity.Get<ProjectilesSpeedStat>();

                var projectileMoveSpeed = projectileEntity.Get<MoveComponent>().Value * moveSpeedStat.BaseValue;

                var projectileDirection = projectileEntity.Get<MoveDirectionComponent>().Direction;

                projectileRigidbody.velocity = projectileDirection * projectileMoveSpeed;
            }
        }
    }
}