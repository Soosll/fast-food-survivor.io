using System.Collections.Generic;
using UnityEngine;

namespace Data.RunTime
{
    public class PlayerChosenAbilitiesData
    {
        public Dictionary<string, int> PlayerActiveAbilities = new();
        public Dictionary<string, int> PlayerPassiveAbilities = new();

        public List<string> AllPlayerAbilitiesId = new();
        
        public int GetActiveAbilityLevelById(string id) => 
            PlayerActiveAbilities.TryGetValue(id, out int value) ? value : 0;
        
        public int GetPassiveAbilityLevelById(string id) => 
            PlayerPassiveAbilities.TryGetValue(id, out int value) ? value : 0;

        public List<string> GetAllAbilitiesId()
        {
            var abilitiesList = new List<string>();

            for (int i = 0; i < AllPlayerAbilitiesId.Count; i++)
                abilitiesList.Add(AllPlayerAbilitiesId[i]);
            
            return abilitiesList;
        }
    }
}