using Leopotam.Ecs;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    sealed class TapListenSystem : BaseSystem, IEcsInitSystem
    {
        private Camera _cam;

        public void Init()
        {
            _cam = Camera.main;
        }

        public override void Run()
        {
            if (!CheckEvent<EventScreenTapDown>())
                return;

            var ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 50f))
            {
                if (hit.collider.gameObject.TryGetComponent<EntityRef>(out var entityRef))
                {
                    if (entityRef.Entity.IsAlive() && entityRef.Entity.Has<EnemyTag>())
                    {
                        entityRef.Entity.Get<DestroyTag>();
                    }
                }
            }
        }

    }
}