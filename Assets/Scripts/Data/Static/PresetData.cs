using Data.Enums;
using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/AbilityPreset", fileName = "PresetData", order = 51)]
    public class PresetData : ScriptableObject
    {
        [field: SerializeField] public AbilityPreset[] AbilitiesPresets { get; private set; }
    }
}