using Abilities.Components.Identification.Active;
using Abilities.Components.Main;
using Data.Enums;
using Leopotam.Ecs;

namespace Abilities.Systems.Identification
{
    public class WeightIdentificationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitAbilityTag, AbilityIdentificationRequest> _identificationAbilitiesFilter;

        private string _abilityId;

        public void Init() => 
            _abilityId = AbilitiesId.Weight.ToString();

        public void Run()
        {
            foreach (int idx in _identificationAbilitiesFilter)
            {
                var requestEntity = _identificationAbilitiesFilter.GetEntity(idx);
                var requestEntityId = requestEntity.Get<AbilityIdentificationRequest>().Id;
                
                if(requestEntityId != _abilityId)
                    continue;
                
                requestEntity.Del<AbilityIdentificationRequest>();
                requestEntity.Get<WeightAbilityTag>();
            }
        }
    }
}