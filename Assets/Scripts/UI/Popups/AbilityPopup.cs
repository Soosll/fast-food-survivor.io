using Mailbox;
using TMPro;
using UI.Presenters;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class AbilityPopup : MonoBehaviour
    {
        [field: SerializeField] public Image AbilityImage { get; private set; }
        [field: SerializeField] public TMP_Text AbilityNameText { get; private set; }
        [field: SerializeField] public TMP_Text DescriptionText { get; private set; }
        [field: SerializeField] public Button ChooseButton { get; private set; }
        [field: SerializeField] public ProgressStarsHolder ProgressStarsHolder { get; private set; }

        public MailboxEvent AbilityClicked { get; private set; } = new ();

        public MailboxValue<string> MailboxValue = new();

        public void Show(IAbilityPresenter abilityPresenter)
        {
            Hide();
            
            AbilityImage.sprite = abilityPresenter.AbilitySprite;
            AbilityNameText.text = abilityPresenter.AbilityName;
            DescriptionText.text = abilityPresenter.AbilityDescription;
            ProgressStarsHolder.Show(abilityPresenter.AbilityLevel);

            MailboxValue.Value = abilityPresenter.AbilityName;
        }

        public void Hide()
        {
            ProgressStarsHolder.Hide();
        }
    }
}