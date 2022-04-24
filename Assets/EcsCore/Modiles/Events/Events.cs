using Leopotam.Ecs;
using UnityEngine;

namespace Modules
{
    public sealed class Events : IEcsRunSystem
    {
        public EcsFilter<OnScreenTapDown> EventsOnScreenTapDown;
        public struct OnScreenTapDown : IEcsIgnoreInFilter {}

        public void Run()
        {
            EventsOnScreenTapDown.IsEmpty();
        }
    }
}