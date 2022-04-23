﻿using Leopotam.Ecs;

namespace Modules.UserInput
{
    // group of object tap receviers (from IPointerDownHandler etc)
    
    
    public struct PointerDown : IEcsIgnoreInFilter { }
    public struct PointerUp : IEcsIgnoreInFilter { }
    public struct PointerClick : IEcsIgnoreInFilter { }
}
