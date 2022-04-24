using Game;
using UnityEngine;

namespace Modules.UserInput
{
    public struct EventScreenTapDown {}
    public struct EventScreenTapUp {}
    public struct EventScreenHold
    {
        // normalised displacement counted by pointer displacement system
        public float XDisplacement;
        public float YDisplacement;
        public bool DragStarted;
    }

    public sealed class TapTrackerSystem : BaseSystem
    {
        public override void Run()
        {
            if (Input.GetMouseButtonUp(0))
                CreateEvent<EventScreenTapUp>();
            else
                RemoveEvent<EventScreenTapUp>();
            
            if (Input.GetMouseButtonDown(0))
                CreateEvent<EventScreenTapDown>();
            else
                RemoveEvent<EventScreenTapDown>();

            if (Input.GetMouseButton(0))
                CreateEvent<EventScreenHold>();
            else
                RemoveEvent<EventScreenHold>();
        }
    }
}








/*
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
*/

/*
            if (Input.GetMouseButtonDown(0) && _down.IsEmpty())
            {
                _ecsWorld.NewEntity().Get<EventScreenTapDown>();

            }
            else if (!_down.IsEmpty())
            {
                foreach (var i in _down)
                {
                    _down.GetEntity(i).Del<EventScreenTapDown>();
                }
            }
*/

/*            
            if (Input.GetMouseButton(0))
            {
                if (_filter.IsEmpty())
                    _ecsWorld.NewEntity().Get<EventScreenHold>();
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
*/            
