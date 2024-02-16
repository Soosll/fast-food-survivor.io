using Data.Enums;
using Data.Static;
using Mailbox;
using UnityEngine;

namespace Data.RunTime
{
    public class InGameData
    {
        public Vector3 JoyStickDirection;

        public MailboxEvent OnGamePhaseChanged { get; } = new();

        public string ChosenPlayerId = "Jimmy";

        public int CurrentGameSecond;
        public int CurrentGameMinute; // на данный момент учитывается время при старте игры
        
        public PlayerData PlayerData;

        private GamePhase _gamePhase;

        public GamePhase GamePhase
        {
            get => _gamePhase;

            set
            {
                _gamePhase = value;
                OnGamePhaseChanged.Invoke();
            }
        }
    }
}