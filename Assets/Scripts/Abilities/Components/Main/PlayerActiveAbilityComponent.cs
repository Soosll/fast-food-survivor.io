using Zun010.MonoLinks;

namespace Abilities.Components.Main
{
    public struct PlayerActiveAbilityComponent
    {
        public string Id;

        public int Level;
        public int Amount;
        public int Pierce;

        public float Area;
        public float Cooldown;
        public float CritMultiplyer;
        public float CritChance;
        public float Damage;
        public float Duration;
        public float HitboxDelay;
        public float Knockback;
        public float Speed;
        
        public MonoEntity ProjectilePrefab;
    }
}