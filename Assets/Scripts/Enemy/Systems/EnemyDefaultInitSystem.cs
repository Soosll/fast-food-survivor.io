using Data.Loaded;
using Enemy.Components;
using General.Components;
using Leopotam.Ecs;
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

                entity.Get<MoveComponent>().Speed = enemyData.DefaultSpeed;
                entity.Get<DamageComponent>().Damage = enemyData.DefaultDamage;
                entity.Get<KcalComponent>().Value = enemyData.DefaultHealth;
                entity.Get<DropExperienceComponent>().Value = enemyData.DefaultExperience;
                
                entity.Del<NotInitEnemyTag>();
                entity.Get<InitEnemyTag>();
            }
        }
    }
}