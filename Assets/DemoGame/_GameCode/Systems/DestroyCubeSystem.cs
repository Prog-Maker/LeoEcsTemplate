using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class DestroyCubeSystem : BaseSystem
    {
        EcsFilter<EnemyTag, DestroyTag, UnityViewComponent> _filterEnemiesToDestroy;

        public override void Run()
        {
            RunFilter<UnityViewComponent>(_filterEnemiesToDestroy, DestroyEnemy);
        }

        private void DestroyEnemy(UnityViewComponent obj, EcsEntity entity)
        {
            Object.Destroy(obj.GameObject);
            entity.Destroy();
        }
    }
}
