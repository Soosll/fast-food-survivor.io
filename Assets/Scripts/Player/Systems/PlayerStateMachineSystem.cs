using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Player.Components.Main;
using Player.Components.Move;
using Zun010.LeoEcsExtensions;

namespace Player.Systems
{
    public class PlayerStateMachineSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private EcsFilter<InitPlayerTag> _playersFilter;

        public void Run()
        {
            foreach (int idx in _playersFilter)
            {
                var playerEntity = _playersFilter.GetEntity(idx);

                var playerDirection = playerEntity.Get<PlayerDirectionComponent>().Direction;

                if(playerEntity.TryGet(out RotateEvent rotateEvent))
                    playerEntity.Del<RotateEvent>();
                
                playerEntity.Get<RotateEvent>().Flip = playerDirection.x > 0;
            }
        }
    }
}