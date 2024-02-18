using System.Collections.Generic;
using System.Linq;
using Data.Static.Abilities;
using UnityEngine;

namespace Data.Loaded
{
    public class AbilitiesLibrary
    {
        private Dictionary<string, ActiveAbilityData> _activeAbilities = new();
        private Dictionary<string, PassiveAbilityData> _passiveAbilities = new();

        public void SetActiveAbilitiesData(Dictionary<string, ActiveAbilityData> activeAbilityDatas) =>
            _activeAbilities = activeAbilityDatas;

        public void SetPassiveAbilitiesData(Dictionary<string, PassiveAbilityData> passiveAbilityDatas) =>
            _passiveAbilities = passiveAbilityDatas;

        public void RemoveActiveElement(string key) => 
            _activeAbilities.Remove(key);

        public void RemovePassiveElement(string id) => 
            _passiveAbilities.Remove(id);

        public ActiveAbilityData ForActiveAbility(string id) => 
            _activeAbilities.TryGetValue(id, out ActiveAbilityData data) ? data : null;

        public PassiveAbilityData ForPassiveAbility(string id) => 
            _passiveAbilities.TryGetValue(id, out PassiveAbilityData data) ? data : null;

        public Dictionary<string, int> GetActiveAbilitiesWeight()
        {
            Dictionary<string, int > allWeights = new Dictionary<string, int>();

            for (int i = 0; i < _activeAbilities.Count; i++)
            {
                var ability = _activeAbilities.ElementAt(i);

                var abilityRarity = (int)ability.Value.Rarity;
                
                allWeights.Add(ability.Key, abilityRarity);
            }

            return allWeights;
        }
        
        public Dictionary<string, int> GetPassiveAbilitiesWeight()
        {
            Dictionary<string, int > allWeights = new Dictionary<string, int>();

            for (int i = 0; i < _passiveAbilities.Count; i++)
            {
                var ability = _passiveAbilities.ElementAt(i);

                var abilityRarity = (int)ability.Value.Rarity;
                
                allWeights.Add(ability.Key, abilityRarity);
            }

            return allWeights;
        }
    }
}