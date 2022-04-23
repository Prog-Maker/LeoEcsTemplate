using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class DestroyCubeSystem : IEcsRunSystem
    {
        EcsFilter<EnemyTag, DestroyTag, UnityViewComponent> _filterEnemiesToDestroy;
        
        void IEcsRunSystem.Run()
        {
            foreach (var enemy in _filterEnemiesToDestroy)
            {
                var entity = _filterEnemiesToDestroy.GetEntity(enemy);

                ref var view = ref entity.Get<UnityViewComponent>();

                Object.Destroy(view.GameObject);

                entity.Destroy();
            }
        }
    }
}
