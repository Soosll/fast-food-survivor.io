using UnityEngine;

namespace UI.Presenters
{
    public interface IAbilityPresenter
    {
        public Sprite AbilitySprite { get; }
        public string AbilityName { get; }
        public string AbilityDescription { get; }
        public int AbilityLevel { get; }
    }
}