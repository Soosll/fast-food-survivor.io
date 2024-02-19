using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelPanel : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Level { get; private set; }
    }
}