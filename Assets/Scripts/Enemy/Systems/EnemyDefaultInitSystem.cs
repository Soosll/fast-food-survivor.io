using Data.Loaded;
using Enemy.Components;
using Experience.Components;
using General.Components;
using General.Components.Parameters;
using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using KcalComponent = Player.Components.KcalComponent;

namespace Enemy.Systems
{
    public class EnemyDefaultInitSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private LoadedData _loadedData;

        private EcsFilter<NotInitEnemyTag> _notInitEnemiesFilter;

        public void Run()
        {
            foreach (int idx in _notInitEnemiesFilter)
            {
                var entity = _notInitEnemiesFilter.GetEntity(idx);

                var enemyId = entity.Get<EnemyIdComponent>().Id;

                var enemyData = _loadedData.EnemiesLibrary.ForEnemy(enemyId);

                entity.Get<MoveComponent>().Value = enemyData.DefaultSpeed;
                entity.Get<DamageComponent>().Value = enemyData.DefaultDamage;
                entity.Get<KcalComponent>().CurrentValue = enemyData.DefaultHealth;
                entity.Get<EnemyAttackRangeComponent>().Value = enemyData.DefaultAttackRange;
                entity.Get<AttackCooldownComponent>().Value = enemyData.DefaultAttackCooldown;
                
               ref var dropExperienceComponent = ref entity.Get<EnemyDropExperienceComponent>();

                dropExperienceComponent.Value = enemyData.DefaultExperience;
                dropExperienceComponent.Prefab = enemyData.ExperiencePrefab;

                entity.Del<NotInitEnemyTag>();
                entity.Get<InitEnemyTag>();
            }
        }
    }
}