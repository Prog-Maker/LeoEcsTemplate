using Leopotam.Ecs;
using Modules.Root;
using UnityEngine;

namespace UICoreECS
{
    public class UICoreSystemsProvider : MonoBehaviour, ISystemsProvider
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private UICoreECS.ScreensCollection _screens;

        public EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems ecsSystems)
        {
            EcsSystems systems = new EcsSystems(world, "UICore");

            systems
                .OneFrame<ScreenUpdateTag>()
                .Add(new ScreenSwitchSystem(_screens, _uiRoot))

                // SD ui =>  moved to SD startup
                // .Add(new Modules.ShootTheDude.UI.LevelInfoDrawer())
                ;

            endFrame
                .OneFrame<UICoreECS.UIUpdate>()
                //.OneFrame<UICoreECS.ScreenUpdateTag>()
                ;

           // world.GetPool<UIScreen>().SetAutoReset(UIScreen.CustomReset);
            return systems;
        }
    }
}
