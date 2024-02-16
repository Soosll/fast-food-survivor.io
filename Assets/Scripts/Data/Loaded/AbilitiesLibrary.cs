using System.Collections.Generic;
using System.Linq;
using Data.Static.Abilities;
using UnityEngine;

namespace Data.Loaded
{
    public class AbilitiesLibrary
    {
        private Dictionary<string, ActiveAbilityData> ActiveAbilities = new();
        private Dictionary<string, PassiveAbilityData> PassiveAbilities = new();

        public void SetActiveAbilitiesData(Dictionary<string, ActiveAbilityData> activeAbilityDatas) =>
            ActiveAbilities = activeAbilityDatas;

        public void SetPassiveAbilitiesData(Dictionary<string, PassiveAbilityData> passiveAbilityDatas) =>
            PassiveAbilities = passiveAbilityDatas;

        public void RemoveActiveElement(string key)
        {
            ActiveAbilities.Remove(key);
        }
        
        public ActiveAbilityData ForActiveAbility(string id) => 
            ActiveAbilities.TryGetValue(id, out ActiveAbilityData data) ? data : null;

        public PassiveAbilityData ForPassiveAbility(string id) => 
            PassiveAbilities.TryGetValue(id, out PassiveAbilityData data) ? data : null;

        public Dictionary<string, int> GetActiveAbilitiesWeight()
        {
            Dictionary<string, int > allWeights = new Dictionary<string, int>();

            for (int i = 0; i < ActiveAbilities.Count; i++)
            {
                var ability = ActiveAbilities.ElementAt(i);

                var abilityRarity = (int)ability.Value.Rarity;
                
                allWeights.Add(ability.Key, abilityRarity);
            }

            return allWeights;
        }
        
        public Dictionary<string, int> GetPassiveAbilitiesWeight()
        {
            Dictionary<string, int > allWeights = new Dictionary<string, int>();

            for (int i = 0; i < PassiveAbilities.Count; i++)
            {
                var ability = PassiveAbilities.ElementAt(i);

                var abilityRarity = (int)ability.Value.Rarity;
                
                allWeights.Add(ability.Key, abilityRarity);
            }

            return allWeights;
        }
    }
}