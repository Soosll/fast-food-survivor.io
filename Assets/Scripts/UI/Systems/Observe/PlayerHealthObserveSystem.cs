using General.Components.Events;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components;
using Player.Components.Main;

namespace UI.Systems.Observe
{
    public class PlayerHealthObserveSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<InitPlayerTag, ReceivedDamageEvent> _damagedPlayersFilter;

        public void Run()
        {
            foreach (int idx in _damagedPlayersFilter)
            {
                var playerEntity = _damagedPlayersFilter.GetEntity(0);

                ref var playerHealthComponent = ref playerEntity.Get<KcalComponent>();
                ref var playerHealthImage = ref playerEntity.Get<HealthImageLink>().Image;

                playerHealthImage.fillAmount = playerHealthComponent.CurrentValue / playerHealthComponent.MaxValue;
            }
        }
    }
}