using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    sealed class CubeCollisionCheck : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<OnTriggerEnterEvent>.Exclude<Destroer> _triggered;
        private readonly EcsFilter<EnemyTag> _enemies;

        void IEcsRunSystem.Run()
        {
            _triggered.ForEach(CheckCubeCollision);
        }

        private void CheckCubeCollision(ref OnTriggerEnterEvent triggeredEvent, ref EcsEntity entitySender)
        {
            foreach (var idx in _enemies)
            {
                var entity = _enemies.GetEntity(idx);

                var view = entity.Get<UnityView>();

                var entityViewOnCollider = triggeredEvent.Collider.GetComponentInParent<EntityView>();

                if (entityViewOnCollider && entityViewOnCollider.Entity.IsAlive() && entityViewOnCollider.Entity.Has<Destroer>())
                {
                    if (view.GameObject == triggeredEvent.SenderGameObject)
                    {
                        entity.Get<DestroyTag>();

                        entitySender.Get<DestroyTag>();
                    }
                }
            }
        }
    }
}