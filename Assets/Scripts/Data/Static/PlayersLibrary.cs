using System;
using System.Linq;
using Data.Enums;
using UnityEngine;
using Zun010.MonoLinks;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/PlayersLibrary", fileName = "PlayersLibrary", order = 51)]
    public class PlayersLibrary : ScriptableObject
    {
        [field: SerializeField] public PlayerData[] PlayersData { get; private set; }
        
        public PlayerData GetDataById(string id) => 
            PlayersData.First(i => i.Id == id);
    }
    
    [Serializable]
    public struct PlayerData
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public float DefaultSpeed { get; private set; }
        [field: SerializeField] public float DefaultKcal { get; private set; }
        [field: SerializeField] public float DefaultTakeExperienceRange { get; private set; }
        [field: SerializeField] public float DefaultLuck { get; private set; }
        [field: SerializeField] public AbilitiesId PlayerBaseAbility { get; private set; }
        [field: SerializeField] public MonoEntity Prefab { get; private set; }
    }
}