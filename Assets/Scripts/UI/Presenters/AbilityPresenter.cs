using UnityEngine;

namespace UI.Presenters
{
    public class AbilityPresenter : IAbilityPresenter
    {
        public Sprite AbilitySprite { get; set; }
        
        public string AbilityName { get; set; }
        public string AbilityDescription { get; set; }
        
        public int AbilityLevel { get; set; }

    }
}