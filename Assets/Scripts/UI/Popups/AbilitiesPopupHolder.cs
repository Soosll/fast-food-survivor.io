using System;
using Extensions;
using UI.Presenters;
using UnityEngine;

namespace UI.Popups
{
    public class AbilitiesPopupHolder : MonoBehaviour
    {
        [field: SerializeField] public AbilityPopup[] AbilitiesPopups { get; private set; }

        private int _currentShownPopups;

        public void Show(IAbilityPresenter presenter)
        {
            AbilitiesPopups[_currentShownPopups].transform.Activate();
            AbilitiesPopups[_currentShownPopups].Show(presenter);
            _currentShownPopups++;
        }


        private void OnDisable()
        {
            foreach (var abilitiesPopup in AbilitiesPopups)
            {
                _currentShownPopups = 0;
                abilitiesPopup.transform.Diactivate();
            }
        }
    }
}