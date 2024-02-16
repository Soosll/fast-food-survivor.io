using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/LevelUpData", fileName = "LevelUpData", order = 51)]
    public class LevelUpData : ScriptableObject
    {
        [field: SerializeField] public int[] LevelExperience { get; private set; }
    }
}