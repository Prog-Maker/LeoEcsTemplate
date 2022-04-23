using Leopotam.Ecs;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    sealed class TapListenSystem : IEcsRunSystem, IEcsInitSystem
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;
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
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }
}