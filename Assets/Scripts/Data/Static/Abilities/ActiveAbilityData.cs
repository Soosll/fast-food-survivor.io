using System;
using System.Collections.Generic;
using Data.Enums;
using Editor;
using UnityEngine;
using Zun010.MonoLinks;

namespace Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "StaticData/Ability/Active", fileName = "ActiveAbilityData", order = 51)]
    public class ActiveAbilityData : ScriptableObject
    {
        public string Id;
       
        public Rarity Rarity;
        
        public Sprite AbilitySprite;
        
        public int MaxLevel;
        
        public MonoEntity ProjectilePrefab;

        public List<ActiveAbilityParameters> ActiveAbilityParameters;
        
        public ActiveAbilityParameters GetByLevel(int level) => 
            ActiveAbilityParameters[level];
    }

    [Serializable]
    public class ActiveAbilityParameters
    {
        public string Description;

        public int Amount = 1;
        
        public float Area = 1;
        public float Cooldown;
        public float CritMultiplyer;
        public float CritChance;
        public float Damage;
        public float Duration;
        public float HitboxDelay;
        public float Knockback;
        public int Pierce = 1;
        public float Speed;

        
        public AbilityParametersForEditorEnum ForEditorEnum;
    }
}