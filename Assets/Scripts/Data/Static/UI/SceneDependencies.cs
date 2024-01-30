using System;
using Cinemachine;
using UI;
using UI.Input;
using UnityEngine;

namespace Data.Static.UI
{
    public class SceneDependencies : MonoBehaviour
    {
        [field: SerializeField] public UIDependencies UIDependencies { get; private set; }
        [field: SerializeField] public CinemachineVirtualCameraBase MainCamera { get; private set; }
    }

    [Serializable]
    public class UIDependencies
    {
        [field: SerializeField] public StartGamePanel StartGamePanel { get; private set; }

        [field: SerializeField] public PlayerHUD PlayerHUD { get; private set; }

    }
}