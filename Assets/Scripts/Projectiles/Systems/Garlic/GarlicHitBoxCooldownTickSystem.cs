using Enemy.Components;
using Leopotam.Ecs;
using Player.Components.Abilities;
using Projectiles.Components.Garlic;
using UnityEngine;

namespace Projectiles.Systems.Garlic
{
    public class GarlicHitBoxCooldownTickSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitEnemyTag, GarlicHitBoxCooldownComponent> _garlicCooldownsFilter;

        public void Run()
        {
            foreach (int idx in _garlicCooldownsFilter)
            {
                var enemyEntity = _garlicCooldownsFilter.GetEntity(idx);

               ref var cooldwonComponent = ref _garlicCooldownsFilter.Get2(idx);

                cooldwonComponent.Value -= Time.deltaTime;

                if (cooldwonComponent.Value <= 0)
                {
                    enemyEntity.Del<GarlicHitBoxCooldownComponent>();
                    enemyEntity.Del<GarlicProjectileAbleHitsComponent>();
                }
            }
        }
    }
}