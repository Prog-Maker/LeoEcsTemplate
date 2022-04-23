using Leopotam.Ecs;

namespace Modules.Utils
{
    public class TimedDestructorSystem : IEcsRunSystem
    {
        //auto injected
        readonly EcsFilter<DestroyTag> _filter;
        readonly Utils.TimeService _time;

        /// <summary>
        /// responcibility - Timed destroer
        /// </summary>
        public void Run() 
        {
            foreach (var i in _filter)
            {
                _filter.Get1(i).DestroyTime -= _time.DeltaTime;
                if(_filter.Get1(i).DestroyTime <= 0.0f) 
                {
                    _filter.GetEntity(i).Destroy();
                }
            }
        }

    }
}
