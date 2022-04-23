using Leopotam.Ecs;
using Modules.Root;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "LeoECS_SystemProviders/TestingProvider", fileName = "TestingProvider")]
    public class TestingProvider : ScriptableObject, ISystemsProvider
    {
        public EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems mainSystems)
        {
            EcsSystems systems = new EcsSystems(world, this.name);

            //systems
                //.Add(new TestSystem)
                
                
                ;

            //endFrame.OneFrame<TestComponent>()
                ;

            return systems;
        }
    }
}