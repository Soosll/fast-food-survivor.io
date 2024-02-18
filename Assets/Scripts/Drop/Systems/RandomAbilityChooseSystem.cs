using System.Collections.Generic;
using System.Linq;
using Data.Enums;
using Data.Loaded;
using Data.RunTime;
using Data.Static;
using Drop.Components;
using Leopotam.Ecs;
using Player.Components;
using Player.Components.Abilities.Main;
using Player.Components.Experience;
using Player.Components.Main;
using Player.Components.Stats;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace Drop.Systems
{
    public class RandomAbilityChooseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private StaticData _staticData;
        private RunTimeData _runTimeData;
        private LoadedData _loadedData;

        private EcsFilter<ChooseAbilityRequest> _playerLevelsUpFilter;
        private EcsFilter<InitPlayerTag, LuckComponent> _playersFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsFilter;

        private Dictionary<string, int> _allActiveAbilities = new ();
        private Dictionary<string, int> _allPassiveAbilities = new();

        private int _popupHideCount;
        
        private bool _allAbilitiesChosen;
        
        public void Run()
        {
            if(_playerLevelsUpFilter.GetEntitiesCount() == 0)
                return;

            var presetData = _loadedData.AbilitiesPresetsLibrary.GetRandomPresetData();

            foreach (int idx in _playerLevelsUpFilter)
            {
                _allActiveAbilities = GetActiveNonChooseAbilitiesWeight(out var activeAbilitiesWeight); 
                _allPassiveAbilities = GetPassiveAbilitiesWeight(out var passiveAbilitiesWeight);
                
                var requestEntity = _playerLevelsUpFilter.GetEntity(idx);
                
                requestEntity.Del<ChooseAbilityRequest>();
                
                var abilitiesPresets = presetData.AbilitiesPresets;

                var playerChosenAbilityData = _runTimeData.PlayerChosenAbilitiesData;
                
                List<string> currentAbilitiesList = _runTimeData.PlayerChosenAbilitiesData.GetAllAbilitiesId();

                var fullyUpgradedSpellsCount = _runTimeData.PlayerChosenAbilitiesData.FullyUpgradedSpellsCount;

                if (fullyUpgradedSpellsCount > abilitiesPresets.Length)
                {
                    _popupHideCount = fullyUpgradedSpellsCount - abilitiesPresets.Length;
                }

                var currentPopupCount = abilitiesPresets.Length - _popupHideCount;
                
                if (playerChosenAbilityData.PlayerActiveAbilities.Count == 3 && playerChosenAbilityData.PlayerPassiveAbilities.Count == 3)
                    _allAbilitiesChosen = true;
                
                if (_allAbilitiesChosen)
                {
                    for (int i = 0; i < currentPopupCount; i++)
                    {
                        var randomAbilityId = GetRandomPlayerAbilityId(currentAbilitiesList);
                        currentAbilitiesList.Remove(randomAbilityId);
                        SendChosenAbility(randomAbilityId);
                        RemoveAbilityIdFromAbleAbilities(randomAbilityId);
                    }
                    
                    continue;
                } // Если абилки заполнены

                if (playerChosenAbilityData.PlayerActiveAbilities.Count == 3)
                    ChangePresetParameters(presetData, AbilityPreset.Passive);

                if (playerChosenAbilityData.PlayerPassiveAbilities.Count == 3)
                    ChangePresetParameters(presetData, AbilityPreset.Active);
                
                for (int i = 0; i < currentPopupCount; i++)
                {
                    var randomAbilityId = TryToGetOwnedAbility(currentAbilitiesList); // Шанс показать способность, уже доступную игроку
                    
                    if (randomAbilityId != null)
                    {
                        SendChosenAbility(randomAbilityId);
                        RemoveAbilityIdFromAbleAbilities(randomAbilityId);
                        currentAbilitiesList.Remove(randomAbilityId);
                        continue;
                    }
                    
                    if (abilitiesPresets[i] == AbilityPreset.Active)
                    {
                        string activeAbilityId = GetRandomActiveAbilityByWeight(_allActiveAbilities, activeAbilitiesWeight);

                        if (activeAbilityId == null)
                        {
                            var random = GetRandomPlayerAbilityId(currentAbilitiesList);
                            SendChosenAbility(random);
                            RemoveAbilityIdFromAbleAbilities(random);
                            currentAbilitiesList.Remove(random);
                            continue;
                        }
                        
                        SendChosenAbility(activeAbilityId);
                        var weightValue = GetAbilityWeightValue(_allActiveAbilities, activeAbilityId);
                        activeAbilitiesWeight -= weightValue;
                        RemoveAbilityIdFromAbleAbilities(activeAbilityId);
                        continue;
                    }
                    
                    if (abilitiesPresets[i] == AbilityPreset.Passive)
                    {
                       string passiveAbilityId = GetRandomPassiveAbilityByWeight(_allPassiveAbilities, passiveAbilitiesWeight);
                       var random = GetRandomPlayerAbilityId(currentAbilitiesList);

                       if (passiveAbilityId == null && random != null)
                       {
                           SendChosenAbility(random);
                           RemoveAbilityIdFromAbleAbilities(random);
                           currentAbilitiesList.Remove(random);
                           continue;
                       }
                       
                       SendChosenAbility(passiveAbilityId);
                       var weightValue = GetAbilityWeightValue(_allPassiveAbilities, passiveAbilityId);
                       passiveAbilitiesWeight -= weightValue;
                       RemoveAbilityIdFromAbleAbilities(passiveAbilityId);
                    }
                }
            }
        }

        private void RemoveAbilityIdFromAbleAbilities(string id)
        {
            if (_allActiveAbilities.TryGetValue(id, out int activeValue))
                _allActiveAbilities.Remove(id);

            if (_allPassiveAbilities.TryGetValue(id, out int passiveValue))
                _allPassiveAbilities.Remove(id);
        }

        private string TryToGetOwnedAbility(List<string> currentAbilitiesList)
        {
            if (currentAbilitiesList.Count == 0)
                return null;
            
            string firstCheckResult = null;
            string secondCheckResult = null;

            if (GetOwnedAbilityChance())
                firstCheckResult = GetRandomPlayerAbilityId(currentAbilitiesList);
            
            else
                return null;

            if (GetOwnedAbilityChance())
                secondCheckResult = GetRandomPlayerAbilityId(currentAbilitiesList);
            
            else
                return null;

            if (firstCheckResult == secondCheckResult)
                return null;

            return firstCheckResult;
        }

        private bool GetOwnedAbilityChance()
        {
            if (_runTimeData.PlayerChosenAbilitiesData.AllPlayerAbilitiesId.Count == 0)
                return false;
            
            var playerEntity = _playersFilter.GetEntity(0);
            var playerStats = _playerStatsFilter.GetEntity(0);
            
            ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>().Level;
            ref var playerLuck = ref playerEntity.Get<LuckComponent>();
            ref var playerLuckStat = ref playerStats.Get<PlayerLuckStat>();

            var playerMultipliedLuckValue = playerLuck.Value * playerLuckStat.ModifiedValue;

            var multiplyer = playerLevel % 2 == 0 ? 1 : 2;

            var chance = (1 + 0.3f * multiplyer - 1 / (1f / 100 * playerMultipliedLuckValue)) * 100;

            var randomValue = Random.Range(0, 100);

            if (chance >= randomValue)
                return true;

            return false;
        }

        private string GetRandomPlayerAbilityId(List<string> currentAbilitiesPool)
        {
            if (currentAbilitiesPool.Count == 0)
                return null;
            
            string randomAbilityId = null;

            randomAbilityId = currentAbilitiesPool[Random.Range(0, currentAbilitiesPool.Count)];

            return randomAbilityId;
        }

        private int GetAbilityWeightValue(Dictionary<string, int> allActiveAbilities, string activeAbilityId) => 
            allActiveAbilities.TryGetValue(activeAbilityId, out int value) ? value : 0;

        private void ChangePresetParameters(PresetData presetData, AbilityPreset newPresetParameter)
        {
            for (int i = 0; i < presetData.AbilitiesPresets.Length; i++)
                presetData.AbilitiesPresets[i] = newPresetParameter;
        }

        private Dictionary<string, int> GetActiveNonChooseAbilitiesWeight(out int activeAbilitiesWeight)
        {
            var playersAbilities = _runTimeData.PlayerChosenAbilitiesData.GetAllAbilitiesId();
            
            var allActiveAbilities = _loadedData.AbilitiesLibrary.GetActiveAbilitiesWeight();

            for (int i = 0; i < playersAbilities.Count; i++)
            {
                allActiveAbilities.Remove(playersAbilities[i]);
            }
            
            activeAbilitiesWeight = allActiveAbilities.Sum(s => s.Value);
            return allActiveAbilities;
        }

        private Dictionary<string, int> GetPassiveAbilitiesWeight(out int passiveAbilitiesWeight)
        {
            var playersAbilities = _runTimeData.PlayerChosenAbilitiesData.GetAllAbilitiesId();

            var allPassiveAbilities = _loadedData.AbilitiesLibrary.GetPassiveAbilitiesWeight();
            
            for (int i = 0; i < playersAbilities.Count; i++)
            {
                allPassiveAbilities.Remove(playersAbilities[i]);
            }
            
            passiveAbilitiesWeight = allPassiveAbilities.Sum(s => s.Value);
            return allPassiveAbilities;
        }

        private string GetRandomActiveAbilityByWeight(Dictionary<string, int> allActiveAbilities, int activeAbilitiesWeight)
        {
            var randomWeight = Random.Range(0, activeAbilitiesWeight);
            
            for (int j = 0; j < allActiveAbilities.Count; j++)
            {
                var abilitiesElement = allActiveAbilities.ElementAt(j);
                randomWeight -= abilitiesElement.Value;

                if (randomWeight <= 0)
                    return abilitiesElement.Key;
            }
            
            return null;
        }

        private string GetRandomPassiveAbilityByWeight(Dictionary<string, int> allPassiveAbilities, int passiveAbilitiesWeight)
        {
            var randomWeight = Random.Range(0, passiveAbilitiesWeight);
            
            for (int j = 0; j < allPassiveAbilities.Count; j++)
            {
                var abilitiesElement = allPassiveAbilities.ElementAt(j);
                randomWeight -= abilitiesElement.Value;

                if (randomWeight <= 0)
                    return abilitiesElement.Key;
            }
            return null;
        }

        private void SendChosenAbility(string chosenPassiveAbilityId)
        {
            var chosenAbilityRequest = _world.NewEntityWith<ChosenRandomAbilityFromPoolRequest>();
            chosenAbilityRequest.Get<ChosenRandomAbilityFromPoolRequest>().ChosenAbilityId = chosenPassiveAbilityId;
        }
    }
}