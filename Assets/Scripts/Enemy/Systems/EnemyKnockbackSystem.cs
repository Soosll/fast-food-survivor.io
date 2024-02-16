using Enemy.Components;
using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;

namespace Enemy.Systems
{
    public class EnemyKnockbackSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<InitEnemyTag, KnockbackEvent> _enemiesToKnockbackFilter;

        public void Run()
        {
            foreach (var idx in _enemiesToKnockbackFilter)
            {
                var enemyEntity = _enemiesToKnockbackFilter.GetEntity(idx);

                ref var knockbackValue = ref enemyEntity.Get<KnockbackEvent>().Value;

                ref var enemyRidgidbody = ref enemyEntity.Get<Rigidbody2DLink>().Rigidbody2D;

                ref var enemyDirection = ref enemyEntity.Get<MoveDirectionComponent>().Direction;
                
                enemyRidgidbody.AddForce(- enemyDirection * (knockbackValue * 100));
                
                enemyEntity.Del<KnockbackEvent>();
            }
        }
    }
}