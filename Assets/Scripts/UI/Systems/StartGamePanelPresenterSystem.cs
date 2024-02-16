using Data.RunTime;
using Data.Static;
using Leopotam.Ecs;
using Mailbox;
using Main.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace UI.Systems
{
    public class StartGamePanelPresenterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private RunTimeData _runTimeData;

        private MailboxEvent.Listener _startGameButtonListener;

        public void Init()
        {
            var startGamePanel = _staticData.SceneDependencies.UIDependencies.StartGamePanel;

            startGamePanel.StartGameButton.onClick.AddListener(() =>
            {
                startGamePanel.StartGameClicked.Invoke();
            });

            _startGameButtonListener = startGamePanel.StartGameClicked.GetListener(false);
        }

        public void Run()
        {
            if(!_startGameButtonListener.IsUnhandled)
                return;

            _startGameButtonListener.MarkAsHandled();
            
            _world.NewEntityWith<StartGameRequest>();
        }
    }
}