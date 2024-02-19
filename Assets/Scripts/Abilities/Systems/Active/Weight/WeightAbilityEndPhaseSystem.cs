using Data.Enums;
using General.Components.Scale;
using Leopotam.Ecs;
using MonoLinks.Links;
using Projectiles.Components.Fall;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace Abilities.Systems.Active.Weight
{
    public class WeightAbilityEndPhaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<FallenProjectileTag> _fellProjectilesFilter;

        private string _abilityId;

        public void Init()
        {
            _abilityId = AbilitiesId.Weight.ToString();
        }

        public void Run()
        {
            foreach (int idx in _fellProjectilesFilter)
            {
                var projectileEntity = _fellProjectilesFilter.GetEntity(idx);
             
                if(projectileEntity.Get<ProjectileTag>().Id != _abilityId)
                    continue;
                
                projectileEntity.Del<FellProjectileTag>();

                ref var projectileEntityTransform = ref projectileEntity.Get<TransformLink>().Transform;
                
                ref var projectileShadow = ref projectileEntity.Get<SpriteRendererReferenceLink>().SpriteRenderer;

                projectileShadow.transform.parent = projectileEntityTransform;
                
                ref var decreaseScaleRequest = ref projectileEntity.Get<DecreaseScaleRequest>();
                
                decreaseScaleRequest.TargetScale = Vector3.zero;
                decreaseScaleRequest.Time = 0.5f;
            }
        }
    }
}