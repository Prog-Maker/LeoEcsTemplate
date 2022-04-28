using Leopotam.Ecs;
using Modules.Root;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "DemoGame/Provider", fileName = "NewProvider")]
    public class DemoGameSystemsProvider : ScriptableObject, ISystemsProvider
    {
        public EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems mainSystems)
        {
            EcsSystems systems = new EcsSystems(world, this.name);

            systems
                .Add(new SpawnCubeSystem())
                .Add(new MoveCubeSystem())
                .Add(new TapListenSystem())
                .Add(new DestroyCubeSystem())

                ;

            endFrame.OneFrame<OnScreenTapDown>()
                ;

            return systems;
        }
    }
}
