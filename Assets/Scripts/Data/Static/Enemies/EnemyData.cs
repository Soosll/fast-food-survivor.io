using UnityEngine;
using Zun010.MonoLinks;

namespace Data.Static.Enemies
{
    [CreateAssetMenu(menuName = "StaticData/Enemy", fileName = "EnemyId", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }

        [field: SerializeField] public float DefaultHealth { get; private set; }
        [field: SerializeField] public float DefaultSpeed { get; private set; }
        [field: SerializeField] public float DefaultDamage { get; private set; }
        
        [field: Range(0, 100)]
        [field: SerializeField] public int DefaultExperience { get; private set; }

        [field: SerializeField] public MonoEntity MonoPrefab { get; private set; }
    }
}