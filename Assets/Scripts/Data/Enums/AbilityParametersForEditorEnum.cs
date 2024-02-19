using System;

namespace Data.Enums
{
    [Flags]
    public enum AbilityParametersForEditorEnum
    {
        Area = 1 << 0,
        Amount = 1 << 1,
        Cooldown= 1 << 2,
        CritMultiplyer= 1 << 3,
        CritChance= 1 << 4,
        Damage= 1 << 5,
        Duration= 1 << 6,
        HitboxDelay= 1 << 7,
        Knockback= 1 << 8,
        Pierce= 1 << 9,
        Speed= 1 << 10,
    }
}