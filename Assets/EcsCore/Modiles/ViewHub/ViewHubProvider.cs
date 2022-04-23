using Leopotam.Ecs;
using Modules.Root;
using UnityEngine;

namespace Modules.ViewHub
{
    [CreateAssetMenu(menuName ="Modules/ViewHub/Provider")]
    public class ViewHubProvider : ScriptableObject, ISystemsProvider
    {
        [SerializeField] private ViewHub _hub;

        public EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems ecsSystems)
        {
            EcsSystems systems = new EcsSystems(world, this.name);
            systems
                .Add(new ViewAllocatorSystem(_hub));

            
            endFrame
                .OneFrame<AllocateView>()
                ;

           //world.GetPool<UnityView>().SetAutoReset(UnityView.CustomReset);

            return systems;
        }
    }
}
