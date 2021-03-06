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
            SharedData sharedData = new SharedData { PrefabsPath = "Items/{0}" };

            systems
                .Add(new SpawnCubeSystem())
                .Add(new MoveCubeSystem())
                .Add(new TapListenSystem())
                .Add(new CubeCollisionCheck())
                .Add(new DestroyGameobjectSystem())
                ;

            endFrame.OneFrame<OnScreenTapDown>()
                ;

            systems.Inject(sharedData);
            return systems;
        }
    }
}
