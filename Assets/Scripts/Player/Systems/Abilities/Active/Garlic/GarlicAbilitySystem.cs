using Data.Enums;
using General.Components;
using General.Components.Parameters;
using General.Components.Scale;
using General.Components.Tags;
using Leopotam.Ecs;
using Player.Components.Abilities.Main;
using Player.Components.Main;
using Player.Components.Stats;
using Projectiles.Components.Garlic;
using Projectiles.Components.General;
using UnityEngine;
using Zun010.LeoEcsExtensions;
using Zun010.MonoLinks;

namespace Player.Systems.Abilities.Active.Garlic
{
    public class GarlicAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter<InitPlayerTag> _playersFilter;
        private EcsFilter<InitAbilityTag>.Exclude<CooldownComponent> _abilitiesFilter;
        private EcsFilter<PlayerStatsTag> _playerStatsEntity;

        private string _abilityId;

        public void Init()
        {
            _abilityId = AbilitiesId.Garlic.ToString();
        }

        public void Run()
        {
            if (_playersFilter.GetEntitiesCount() == 0)
                return;

            var playerEntity = _playersFilter.GetEntity(0);

            foreach (int idx in _abilitiesFilter)
            {
                var abilityEntity = _abilitiesFilter.GetEntity(idx);

                ref var activeAbilityComponent = ref abilityEntity.Get<PlayerActiveAbilityComponent>();

                if (activeAbilityComponent.Id != _abilityId)
                    continue;

                var statsEntity = _playerStatsEntity.GetEntity(0);
                
                ref var playerTransform = ref playerEntity.Get<TransformLink>().Transform;
                
                int dividedCircleProjectileSpace = 360 / activeAbilityComponent.Amount;

                float currentRotationDegree = 0;

                for (int i = 0; i < activeAbilityComponent.Amount; i++)
                {
                    var garlicEntity = _world.NewEntityWith<ProjectileTag>();

                    HangComponentsToEntity(garlicEntity, activeAbilityComponent);

                    ref var moveAroundComponent = ref garlicEntity.Get<MoveSpeedAroundComponent>();
                    ref var moveStat = ref statsEntity.Get<ProjectilesSpeedStat>();
                    
                    moveAroundComponent.Speed = activeAbilityComponent.Speed * moveStat.ModifiedValue / 10;
                    currentRotationDegree += dividedCircleProjectileSpace;

                    var spawnPoint = GetSpawnPoint(playerTransform, currentRotationDegree, activeAbilityComponent.Area);

                    var monoEntity = Object.Instantiate(activeAbilityComponent.ProjectilePrefab, spawnPoint, Quaternion.identity);
                    monoEntity.transform.parent = playerTransform;
                    monoEntity.Make(ref garlicEntity);

                    SendIncreaseEvent(garlicEntity);
                }

                abilityEntity.Get<CooldownComponent>().Value = activeAbilityComponent.Cooldown + activeAbilityComponent.Duration;
            }
        }

        private static void HangComponentsToEntity(EcsEntity garlicEntity, PlayerActiveAbilityComponent activeAbilityComponent)
        {
            garlicEntity.Get<GarlicProjectileTag>();
            garlicEntity.Get<ProjectileTag>().Id = activeAbilityComponent.Id;
            garlicEntity.Get<DurationComponent>().Value = activeAbilityComponent.Duration;
            garlicEntity.Get<GarlicProjectileAbleHitsComponent>().AbleHits = activeAbilityComponent.Amount;
            garlicEntity.Get<DamageComponent>().Value = activeAbilityComponent.Damage;
            garlicEntity.Get<KnockbackComponent>().Value = activeAbilityComponent.Knockback;
            garlicEntity.Get<CritComponent>().CritChance = activeAbilityComponent.CritChance;
            garlicEntity.Get<CritComponent>().CritMultiplyer = activeAbilityComponent.CritMultiplyer;
            garlicEntity.Get<HitBoxDelayComponent>().Value = activeAbilityComponent.HitboxDelay;
            garlicEntity.Get<BlockRotateTag>();
        }

        private static void SendIncreaseEvent(EcsEntity garlicEntity)
        {
            ref var garlicTransform = ref garlicEntity.Get<TransformLink>().Transform;

            garlicEntity.Get<IncreaseScaleRequest>().TargetScale = garlicTransform.localScale;
            garlicEntity.Get<IncreaseScaleRequest>().Time = 1.5f;

            garlicTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        private Vector3 GetSpawnPoint(Transform playerTransform, float currentRotationDegree, float area) =>
            playerTransform.position + new Vector3(Mathf.Cos(currentRotationDegree * Mathf.Deg2Rad),
                Mathf.Sin(currentRotationDegree * Mathf.Deg2Rad), 0).normalized * area;
    }
}