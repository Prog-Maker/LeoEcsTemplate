using Leopotam.Ecs;

namespace Game 
{
    sealed class MoveCubeSystem : IEcsRunSystem
    {
        EcsFilter<EnemyTag, Movable, SpeedComponent, UnityViewComponent> _enemyFilter;
        
        void IEcsRunSystem.Run ()
        {
            foreach (var enemy in _enemyFilter)
            {
                var entity = _enemyFilter.GetEntity(enemy);

                ref var view = ref entity.Get<UnityViewComponent>();
                ref var speed = ref entity.Get<SpeedComponent>();

                view.Transform.Translate(-view.Transform.up * speed.SpeedValue * UnityEngine.Time.deltaTime);
            }
        }
    }
}