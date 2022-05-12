using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class DestroyGameobjectSystem : IEcsRunSystem
    {
        EcsFilter<DestroyTag> _filterEnemiesToDestroy;
        
        void IEcsRunSystem.Run()
        {
            _filterEnemiesToDestroy.ForEach(Destroy);
        }

        private void Destroy(ref EcsEntity entity)
        {
            if (entity.Has<UnityView>())
            {
                ref var view = ref entity.Get<UnityView>();
                Object.Destroy(view.GameObject);
            }

            entity.Destroy();
        }
    }
}
