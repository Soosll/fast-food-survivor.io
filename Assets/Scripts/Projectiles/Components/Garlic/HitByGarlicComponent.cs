using Leopotam.Ecs;

namespace Projectiles.Components.Garlic
{
    public struct HitByGarlicComponent
    {
        public EcsEntity LastHitEntity;

        public int HitsCount;
    }
}