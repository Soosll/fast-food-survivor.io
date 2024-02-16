using Data.Enums;
using Extensions;
using General.Components.Scale;
using General.Components.Tags;
using Leopotam.Ecs;
using MonoLinks.Links;
using Player.Components.Abilities.Main;
using Projectiles.Components.Fall;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.MonoLinks;

namespace Player.Systems.Abilities.Active.Weight
{
    public class WeightAbilityEndPhaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitAbilityTag> _abilitiesFilter;
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
                
                decreaseScaleRequest.TargetScale = new Vector3().MinimalObjectScale();
                decreaseScaleRequest.Time = 0.5f;
                decreaseScaleRequest.Delay = 0.5f;
            }
        }
    }
}