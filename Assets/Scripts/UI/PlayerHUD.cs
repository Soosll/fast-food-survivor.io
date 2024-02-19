using UI.Input;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [field: SerializeField] public JoyStick JoyStick { get; private set; }
        [field: SerializeField] public ExperiencePanel ExperiencePanel { get; private set; }
        [field: SerializeField] public LevelPanel LevelPanel { get; private set; }
    }
}