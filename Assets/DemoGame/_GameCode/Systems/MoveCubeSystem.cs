using Leopotam.Ecs;
using Modules.Root;
using UnityEngine;

namespace Game 
{
    sealed class MoveCubeSystem : IEcsRunSystem
    {
        EcsFilter<EnemyTag, Movable, SpeedComponent, UnityView> _enemyFilter;

        private SharedData sharedData;

        void IEcsRunSystem.Run ()
        {
            foreach (var enemy in _enemyFilter)
            {
               // Debug.Log(sharedData.PrefabsPath);
                
                var entity = _enemyFilter.GetEntity(enemy);

                ref var view = ref entity.Get<UnityView>();
                ref var speed = ref entity.Get<SpeedComponent>();

                view.Transform.Translate(-view.Transform.up * speed.SpeedValue * UnityEngine.Time.deltaTime);
            }
        }
    }

    class SharedData
    {
        public string PrefabsPath;
    }
}