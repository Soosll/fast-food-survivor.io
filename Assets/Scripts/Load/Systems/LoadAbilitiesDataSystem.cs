using System.Linq;
using Data;
using Data.Loaded;
using Data.Static;
using Data.Static.Abilities;
using Leopotam.Ecs;
using UnityEngine;

namespace Load.Systems
{
    public class LoadAbilitiesDataSystem : IEcsInitSystem
    {
        public LoadedData _loadedData;
        
        public void Init()
        {
            LoadActiveAbilitiesData();
            LoadPassiveAbilitiesData();
        }

        private void LoadPassiveAbilitiesData()
        {
            var passiveAbilitiesData = Resources
                .LoadAll<PassiveAbilityData>(DataPath.PassiveAbilitiesDataPath)
                .ToDictionary(p => p.Id, x => x);
            
            _loadedData.AbilitiesLibrary.SetPassiveAbilitiesData(passiveAbilitiesData); 
        }

        private void LoadActiveAbilitiesData()
        {
            var activeAbilities = Resources
                .LoadAll<ActiveAbilityData>(DataPath.ActiveAbilitiesDataPath)
                .ToDictionary(a => a.Id, a => a);
            
            _loadedData.AbilitiesLibrary.SetActiveAbilitiesData(activeAbilities); 
        }
    }
}