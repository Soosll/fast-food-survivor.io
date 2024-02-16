using System.Linq;
using Data;
using Data.Loaded;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;

namespace Load.Systems
{
    public class LoadAbilitiesPresetsDataSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        private LoadedData _loadedData;
        
        public void Init()
        {
            var presetsData = Resources.LoadAll<PresetData>(DataPath.AbilitiesPresetsDataPath).ToList();
            
          _loadedData.AbilitiesPresetsLibrary.SetPresetsData(presetsData);     
        }
    }
}