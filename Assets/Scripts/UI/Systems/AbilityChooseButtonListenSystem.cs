using System.Collections.Generic;
using Data.Static;
using Leopotam.Ecs;
using Mailbox;
using UI.Components;
using UnityEngine;
using Zun010.LeoEcsExtensions;

namespace UI.Systems
{
    public class AbilityChooseButtonListenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        
        private List<MailboxEvent.Listener> _listeners = new ();

        public void Init()
        {
            var abilitiesPopups = _staticData.SceneDependencies.UIDependencies.AbilitiesPopupHolder.AbilitiesPopups;

            for (int i = 0; i < abilitiesPopups.Length; i++)
            {
                var currentPopup = abilitiesPopups[i];
                
                currentPopup.ChooseButton.onClick.AddListener(() =>
                {
                    currentPopup.AbilityClicked.Invoke();
                });

                _listeners.Add(abilitiesPopups[i].AbilityClicked.GetListener(false));
            }
        }

        public void Run()
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                if(!_listeners[i].IsUnhandled)
                    continue;
                
                _listeners[i].MarkAsHandled();

                var mailboxValue = _staticData.SceneDependencies.UIDependencies.AbilitiesPopupHolder.AbilitiesPopups[i].MailboxValue.Value;
                var chooseAbilityEventEntity = _world.NewEntityWith<AbilityChooseEvent>();
                chooseAbilityEventEntity.Get<AbilityChooseEvent>().ChosenAbilityId = mailboxValue;
            }
        }
    }
}