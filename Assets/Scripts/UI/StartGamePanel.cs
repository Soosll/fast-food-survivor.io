using Mailbox;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGamePanel : MonoBehaviour
    {
        [field: SerializeField] public Button StartGameButton { get; private set; }

        [field: SerializeField] public MailboxEvent StartGameClicked { get; private set; } = new();
    }
}