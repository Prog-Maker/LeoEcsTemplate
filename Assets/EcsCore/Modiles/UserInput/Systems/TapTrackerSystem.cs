using Leopotam.Ecs;
using UnityEngine;

namespace Modules.UserInput
{
    public sealed class TapTrackerSystem : IEcsRunSystem
    {
        private EcsFilter<OnScreenTapUp> _up;
        private EcsFilter<OnScreenTapDown> _down;

        private EcsWorld _ecsWorld;
        private EcsFilter<OnScreenHold> _filter;

        public void Run()
        {
            if (!_up.IsEmpty())
            {
                foreach (var i in _up)
                {
                    _up.GetEntity(i).Del<OnScreenTapUp>();
                }
            }

            if (Input.GetMouseButtonUp(0) && _up.IsEmpty())
            {
                _ecsWorld.NewEntity().Get<OnScreenTapUp>();

            }
            else if (!_up.IsEmpty())
            {
                foreach (var i in _up)
                {
                    _up.GetEntity(i).Del<OnScreenTapUp>();
                }
            }

            if (Input.GetMouseButtonDown(0) && _down.IsEmpty())
            {
                _ecsWorld.NewEntity().Get<OnScreenTapDown>();

            }
            else if (!_down.IsEmpty())
            {
                foreach (var i in _down)
                {
                    _down.GetEntity(i).Del<OnScreenTapDown>();
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_filter.IsEmpty())
                    _ecsWorld.NewEntity().Get<OnScreenHold>();
            }
            else
            {
                if (!_filter.IsEmpty())
                {
                    foreach (var i in _filter)
                    {
                        _filter.GetEntity(i).Destroy();
                    }
                }
            }
        }
    }
}