﻿using Game;
using Leopotam.Ecs;
using UnityEngine;

namespace Modules.UserInput
{
    /// <summary>
    /// counts pointer displacement
    /// </summary>
    public class PointerDisplacementSystem : BaseSystem
    {
        // auto injected
        readonly EcsFilter<EventScreenHold> _hold;

        readonly float _minToDrag; 

        private float prevX;
        private float prevY;
        private float downX;
        private float downY;

        public PointerDisplacementSystem(float minToDrag) 
        {
            _minToDrag = minToDrag;
        }

        public override void Run() 
        {
            if (CheckEvent<EventScreenTapDown>()) 
            {
                // reset
                prevX = Input.mousePosition.x / Screen.width;
                prevY = Input.mousePosition.y / Screen.height;
                downX = prevX;
                downY = prevY;
            }

            if (CheckEvent<EventScreenHold>())
            {
                foreach (var i in _hold)
                {
                    // calculate displacement
                    _hold.Get1(i).XDisplacement = Input.mousePosition.x / Screen.width - prevX;
                    _hold.Get1(i).YDisplacement = Input.mousePosition.y / Screen.height - prevY;
                    if (!_hold.Get1(i).DragStarted) 
                    {
                        if(Mathf.Abs(downX -(prevX + _hold.Get1(i).XDisplacement))
                            + Mathf.Abs(downY - (prevY + _hold.Get1(i).YDisplacement)) > _minToDrag)
                        {
                            _hold.GetEntity(i).Get<OnDragStarted>();
                            _hold.Get1(i).DragStarted = true;
                        }
                    }
                }

                // update prev pos
                prevX = Input.mousePosition.x / Screen.width;
                prevY = Input.mousePosition.y / Screen.height;
            }

            if (!CheckEvent<EventScreenTapUp>())
            {
                // reset
                prevX = Input.mousePosition.x / Screen.width;
                prevY = Input.mousePosition.y / Screen.height;
            }
        }

    }
}
