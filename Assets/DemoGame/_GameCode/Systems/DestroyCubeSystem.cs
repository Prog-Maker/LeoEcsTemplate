using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class DestroyGameobjectSystem : IEcsRunSystem
    {
        EcsFilter<DestroyTag> _filterEnemiesToDestroy;
        
        void IEcsRunSystem.Run()
        {
            foreach (var enemy in _filterEnemiesToDestroy)
            {
                var entity = _filterEnemiesToDestroy.GetEntity(enemy);

                if (entity.Has<UnityView>())
                {
                    ref var view = ref entity.Get<UnityView>();

                    Object.Destroy(view.GameObject);
                }

                entity.Destroy();
            }
        }
    }
}
