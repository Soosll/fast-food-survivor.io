using Data;
using Data.Loaded;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;

namespace Load.Systems
{
    public class LoadLevelUpDataSystem : IEcsInitSystem
    {
        private LoadedData _loadedData;
        
        public void Init()
        {
            var data = Resources.Load<LevelUpData>(DataPath.LevelUpDataPath);
            _loadedData.LevelUpLibrary.SetData(data);
        }
    }
}