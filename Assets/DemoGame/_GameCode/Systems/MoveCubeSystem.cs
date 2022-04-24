using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    sealed class MoveCubeSystem : BaseSystem
    {
        EcsFilter<EnemyTag, Movable, SpeedComponent, UnityViewComponent> _enemyFilter;

        public override void Run()
        {
            RunFilter(_enemyFilter, Move);
            RunFilter<SpeedComponent>(_enemyFilter, SetSpeed);
        }

        private void Move(ref EcsEntity entity)
        {
            ref var view = ref entity.Get<UnityViewComponent>();
            ref var speed = ref entity.Get<SpeedComponent>();

            view.Transform.Translate(-view.Transform.up * speed.SpeedValue * UnityEngine.Time.deltaTime);
        }
        
        private void SetSpeed(ref SpeedComponent speedComponent)
        {
            speedComponent.SpeedValue += Time.deltaTime;
        }
        
    }
}