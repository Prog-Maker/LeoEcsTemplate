using Leopotam.Ecs;

namespace Modules.Utils
{
    public class LiveTimeSystem : IEcsRunSystem
    {
        readonly EcsFilter<LiveTime> _filter;
        readonly TimeService _time;

        public void Run() 
        {
            foreach (var i in _filter)
            {
                _filter.Get1(i).RemainingTime -= _time.DeltaTime;
                if(_filter.Get1(i).RemainingTime <= 0.0f) 
                {
                    _filter.GetEntity(i).Destroy();
                }
            }
        }

    }
}
