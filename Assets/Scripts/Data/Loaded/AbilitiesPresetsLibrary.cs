using System.Collections.Generic;
using Data.Static;
using UnityEngine;

namespace Data.Loaded
{
    public class AbilitiesPresetsLibrary
    {
        private List<PresetData> PresetsData = new();

        public void SetPresetsData(List<PresetData> presetsData) => 
            PresetsData = presetsData;

        public PresetData GetRandomPresetData() => 
            PresetsData[Random.Range(0, PresetsData.Count)];
    }
}