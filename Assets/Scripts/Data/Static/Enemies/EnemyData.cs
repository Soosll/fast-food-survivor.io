using UnityEngine;
using Zun010.MonoLinks;

namespace Data.Static.Enemies
{
    [CreateAssetMenu(menuName = "StaticData/EnemyData", fileName = "EnemyData", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }

        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        
        [field: Range(0, 100)]
        [field: SerializeField] public int Experience { get; private set; }

        [field: SerializeField] public MonoEntity MonoPrefab { get; private set; }
    }
}