using UnityEngine;
using Zun010.MonoLinks;

namespace Data.Static
{
    [CreateAssetMenu(menuName = "StaticData/SpawnersData", fileName = "Spawners Data", order = 51)]
    public class SpawnersData : ScriptableObject
    {
        public int GeneralCount => TopCount + BotCount + RightCount + LeftCount;
        
        public int TopCount;
        public int BotCount;
        public int RightCount;
        public int LeftCount;

        public float OffsetFromCameraBounds;

        public MonoEntity SpawnerMonoPrefab;
    }
}