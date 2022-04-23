using Leopotam.Ecs;
using UnityEngine;

namespace UICoreECS
{
    public class ScreenSwitchSystem : IEcsRunSystem
    {
        // auto injected fields
        readonly EcsFilter<ShowScreenTag> _filter;
        readonly EcsFilter<UIScreen> _screens;
        readonly EcsWorld _world;

        private bool _screenFound;
        readonly ScreensCollection _screensCollection;
        readonly Transform _screensRoot;

        public ScreenSwitchSystem(ScreensCollection screensCollection, Transform screensRoot)
        {
            _screenFound = false;
            _screensCollection = screensCollection;
            _screensRoot = screensRoot;
        }

        public void Run()
        {
            if(_filter.IsEmpty())
                return;

            foreach (var i in _filter)
            {
                _screenFound = false;

                foreach (var j in _screens)
                {
                    if(_screens.Get1(j).Layer == _filter.Get1(i).Layer)
                    {
                        if (_screens.Get1(j).ID == _filter.Get1(i).ID)
                        {
                            _screens.Get1(j).Active = true;
                            _screens.Get1(j).Screen.Show();
                            _screens.GetEntity(j).Get<ScreenUpdateTag>();
                            _screenFound = true;                            
                        }
                        else if(_screens.Get1(j).Active)
                        {
                            _screens.Get1(j).Active = false;
                            _screens.Get1(j).Screen.Hide();
                        }
                    }
                }

                // spawn screen(if -100 do not show)
                if(!_screenFound && _filter.Get1(i).ID != -100)
                {
                    EcsEntity target =  _world.NewEntity();
                    ECSUIScreen screen = GameObject.Instantiate(_screensCollection.GetScreen(_filter.Get1(i).ID, _filter.Get1(i).Layer), _screensRoot)
                        .Init(_world,target, _filter.Get1(i).ID, _filter.Get1(i).Layer);
                    ref UIScreen entity = ref target.Get<UIScreen>();
                    entity.ID = _filter.Get1(i).ID;
                    entity.Layer = _filter.Get1(i).Layer;
                    entity.Active = true;
                    entity.Screen = screen;
                    screen.Show();
                    target.Get<ScreenUpdateTag>();
                }

                _world.NewEntity().Get<UIUpdate>();
                _filter.GetEntity(i).Destroy(); // cleanup
            }
        }
    }
}