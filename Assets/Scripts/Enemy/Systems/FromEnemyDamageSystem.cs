using General.Components.Events;
using General.Components.Tags;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Main;
using UnityEngine;

namespace Enemy.Systems
{
    public class FromEnemyDamageSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitPlayerTag, TakeDamageEvent> _playersFilter;
        
        public void Run()
        {
            if(_playersFilter.GetEntitiesCount() == 0)
                return;

            foreach (int idx in _playersFilter)
            {
                var playerEntity = _playersFilter.GetEntity(0);

                
                ref var currentPlayerHealth = ref playerEntity.Get<KcalComponent>().CurrentValue;

                ref var damageToPlayer = ref playerEntity.Get<TakeDamageEvent>().Value;
                
                currentPlayerHealth -= damageToPlayer;
                
                playerEntity.Del<TakeDamageEvent>();

                if (currentPlayerHealth <= 0)
                    playerEntity.Get<DeathTag>();

                else
                    playerEntity.Get<ReceivedDamageEvent>();
            }
        }
    }
}