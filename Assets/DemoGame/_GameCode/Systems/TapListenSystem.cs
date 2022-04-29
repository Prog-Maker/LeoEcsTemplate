using Leopotam.Ecs;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    sealed class TapListenSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsFilter<OnScreenTapDown> _filter;

        private Camera _cam;

        public void Init()
        {
            _cam = Camera.main;
        }

        void IEcsRunSystem.Run()
        {
            if (!_filter.IsEmpty())
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 50f))
                {
                    if (hit.collider.gameObject.TryGetComponent<EntityView>(out var entityView))
                    {
                        if (entityView.Entity.IsAlive() && entityView.Entity.Has<EnemyTag>())
                        {
                            entityView.Entity.Get<DestroyTag>();
                        }
                    }
                }
            }
        }
    }
}