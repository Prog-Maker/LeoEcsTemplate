using System;
using Leopotam.Ecs;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    public class BaseSystem : IEcsRunSystem
    {
        public EcsWorld _ecsWorld;
        
        public virtual void Run()
        {
        }

        public void RunFilter(EcsFilter filter, Action<EcsEntity> action)
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                action(entity);
            }
        }

        public void RunFilter<T>(EcsFilter filter, int index, Action<T> action) where T : struct
        {
            ref var entity = ref filter.GetEntity(index);
            action(entity.Get<T>());
        }

        public void RunFilter<T>(EcsFilter filter, Action<T> action) where T : struct
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                action(entity.Get<T>());
            }
        }

        public void RunFilter<T>(EcsFilter filter, Action<T, EcsEntity> action) where T : struct
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                action(entity.Get<T>(), entity);
            }
        }

        public void RunFilter<T1, T2>(EcsFilter filter, Action<T1, T2> action) where T1 : struct where T2 : struct
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                action(entity.Get<T1>(), entity.Get<T2>());
            }
        }

        public T GetObject<T>(EcsFilter filter, int index) where T : struct
        {
            ref var entity =  ref filter.GetEntity(index);
            return entity.Get<T>();
        }

        public void DestroyEntity<T>(EcsFilter filter)
        {
            
        }

        public void CreateEvent<T>() where T : struct
        {
            EcsFilter ecsFilter = _ecsWorld.GetFilter(typeof(EcsFilter<T>));
            
            if (ecsFilter.IsEmpty())
            {
                _ecsWorld.NewEntity().Get<T>();
            }
        }

        public void RemoveEvent<T>() where T : struct
        {
            EcsFilter ecsFilter = _ecsWorld.GetFilter(typeof(EcsFilter<T>));

            if (ecsFilter.IsEmpty())
                return;
            
            foreach (var i in ecsFilter)
            {
                ecsFilter.GetEntity(i).Del<T>();
            }
        }
        
        public bool CheckEvent<T>() where T : struct
        {
            EcsFilter ecsFilter = _ecsWorld.GetFilter(typeof(EcsFilter<T>));
            return !ecsFilter.IsEmpty();
        }

        public EcsFilter GetFilter<T>() where T : struct
        {
            EcsFilter ecsFilter = _ecsWorld.GetFilter(typeof(EcsFilter<T>));
            return ecsFilter;
        }

    }
}
