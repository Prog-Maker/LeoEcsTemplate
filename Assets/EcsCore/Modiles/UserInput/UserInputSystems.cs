using UnityEngine;
using Modules.Root;
using Leopotam.Ecs;

namespace Modules.UserInput
{
    [CreateAssetMenu(menuName = "Modules/UserInput/Provider")]
    public class UserInputSystems : ScriptableObject, ISystemsProvider
    {
        [SerializeField] private float _minToDrag;

        public EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems ecsSystems)
        {
            EcsSystems systems = new EcsSystems(world, this.name);

            systems
                .Add(new TapTrackerSystem())
                .Add(new PointerDisplacementSystem(_minToDrag))       // count displacement
                ;

            endFrame
                .OneFrame<PointerDown>()
                .OneFrame<PointerClick>()
                .OneFrame<PointerUp>()
                .OneFrame<OnDragStarted>()
                ;

            return systems;
        }
    }
}
