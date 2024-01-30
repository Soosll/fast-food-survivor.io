using UI.Input;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [field: SerializeField] public JoyStick JoyStick { get; private set; }
    }
}