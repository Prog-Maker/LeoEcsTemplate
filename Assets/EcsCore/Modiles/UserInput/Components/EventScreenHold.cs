using Leopotam.Ecs;

namespace Modules.UserInput
{
    public struct EventScreenHold2
    {
        // normalised displacement counted by pointer displacement system
        public float XDisplacement;
        public float YDisplacement;
        public bool DragStarted;
    }
}