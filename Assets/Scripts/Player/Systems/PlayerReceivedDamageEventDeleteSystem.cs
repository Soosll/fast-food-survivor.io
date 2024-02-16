using General.Components.Events;
using Leopotam.Ecs;
using Player.Components.Main;

namespace Player.Systems
{
    public class PlayerReceivedDamageEventDeleteSystem : IEcsRunSystem
    {
        private EcsFilter<InitPlayerTag, ReceivedDamageEvent> _damagedPlayersFilter;

        public void Run()
        {
            foreach (int idx in _damagedPlayersFilter)
            {
                var playerEntity = _damagedPlayersFilter.GetEntity(0);
                
                playerEntity.Del<ReceivedDamageEvent>();
            }
        }
    }
}