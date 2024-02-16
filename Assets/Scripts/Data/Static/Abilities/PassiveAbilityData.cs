using System;
using System.Collections.Generic;
using System.Linq;
using Data.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "StaticData/Ability/Passive", fileName = "PassiveAbilityName", order = 51)]
    public class PassiveAbilityData : ScriptableObject
    {
        public string Id;
        
        public Sprite AbilitySprite;

        public Rarity Rarity;
        
        public int MaxLevel;
        
        public List<PassiveAbilityParameters> PassiveAbilityParameters;

        public PassiveAbilityParameters GetByLevel(int level) =>
            PassiveAbilityParameters[level];
    }

    [Serializable]
    public class PassiveAbilityParameters
    {
        public string Description;
       
        public bool InPercent;
       
        public float Value;
    }
}