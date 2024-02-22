using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/MapBorders", fileName = "MapBordersData", order = 51)]
    public class MapBordersData : ScriptableObject
    {
        [field: SerializeField] public float BorderX { get; private set; }
        [field: SerializeField] public float BorderY { get; private set; }
    }
}