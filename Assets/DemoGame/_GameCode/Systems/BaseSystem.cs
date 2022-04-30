using System;
using Leopotam.Ecs;
using Modules.UserInput;
using UnityEngine;

namespace Game
{
    public class BaseSystem : IEcsRunSystem
    {
        public EcsWorld _ecsWorld;

        public delegate void ActionRef<T>(ref T component);
        public delegate void ActionEntityRef<EcsEntity>(ref EcsEntity entity);
        public delegate void ActionEntityRef2<T, EcsEntity>(ref T component, ref EcsEntity entity);
        
        public virtual void Run()
        {
        }

        public void RunFilter(EcsFilter filter, ActionEntityRef<EcsEntity> actionEntity)
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                actionEntity(ref entity);
            }
        }

        public void RunFilter<T>(EcsFilter filter, ActionRef<T> action) where T : struct 
        {
            foreach (int i in filter)
            {
                ref var entity =  ref filter.GetEntity(i);
                action(ref entity.Get<T>());
            }
        }

        public void RunFilter<T>(EcsFilter filter, ActionEntityRef2<T, EcsEntity> action) where T : struct
        {
            foreach (int i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                action(ref entity.Get<T>(), ref entity);
            }
        }

        public T GetObject<T>(EcsFilter filter, int index) where T : struct
        {
            ref var entity =  ref filter.GetEntity(index);
            ref T comp = ref entity.Get<T>();
            return comp;
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
