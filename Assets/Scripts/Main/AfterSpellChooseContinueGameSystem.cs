using Data.Enums;
using Data.RunTime;
using Leopotam.Ecs;
using Main.Components;
using UnityEngine;

namespace Main
{
    public class AfterSpellChooseContinueGameSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private RunTimeData _runTimeData;
        
        private EcsFilter<AfterSpellChooseContinueGameRequest> _afterSpellChooseContinueGameRequestFilter;

        public void Run()
        {
            if(_afterSpellChooseContinueGameRequestFilter.GetEntitiesCount() == 0)
                return;
            
            foreach (int idx in _afterSpellChooseContinueGameRequestFilter)
            {
                var requestEntity = _afterSpellChooseContinueGameRequestFilter.GetEntity(idx);
                
                requestEntity.Del<AfterSpellChooseContinueGameRequest>();

                _runTimeData.InGameData.GamePhase = GamePhase.AfterSpellChoose;
            }
        }
    }
}