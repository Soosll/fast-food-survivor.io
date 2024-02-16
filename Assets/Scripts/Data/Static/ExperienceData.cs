using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/Experience", fileName = "ExperienceData", order = 51)]
    public class ExperienceData : ScriptableObject
    {
        [field: Header("From Player")]
        [field: Tooltip("Расстояние, на которое опыт отдалится от героя прежде, чем начать лететь к нему")]
        [field: SerializeField] public float MoveDistanceFrom { get; private set; }
        
        [field: Tooltip("Скорость, с которой опыт отдалится от героя прежде, чем начать лететь к нему")]
        [field: SerializeField] public float MoveSpeedFrom { get; private set; }
        
        [field: Header("To Player")]
        [field: Tooltip("Скорость движения объекта опыта к игроку после того, как он отдалился назад")]
        [field: SerializeField] public float MoveSpeedTo { get; private set; }
    }
}