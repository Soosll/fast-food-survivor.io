using Data.Static.UI;
using UnityEngine;

namespace Data.Static
{
    public class StaticData : MonoBehaviour
    {
        [field: SerializeField] public PlayersLibrary PlayersLibrary { get; private set; }
        [field: SerializeField] public SceneDependencies SceneDependencies { get; private set; }

        [field: SerializeField] public ExperienceData ExperienceData { get; private set; }
    }
}