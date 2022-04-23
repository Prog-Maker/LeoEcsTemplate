using Leopotam.Ecs;

namespace Game 
{
    sealed class SpawnObjectsSystem : IEcsRunSystem
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        
        void IEcsRunSystem.Run ()
        {
            // add your run code here.
        }
    }
}