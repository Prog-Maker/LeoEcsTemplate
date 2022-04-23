using System.Collections.Generic;
using Leopotam.Ecs;
using Modules.ViewHub.Interfaces;
using UnityEngine;

namespace Modules.ViewHub
{
    public class ViewElement : MonoBehaviour, IUViewElement
    {
        public string id;
        public List<ViewComponent> _components;

        public void Allocate(EcsEntity entity, EcsWorld world)
        {
            Object.Instantiate(this).Spawn(entity, world);
        }

        public string ID => id;

        public void Spawn(EcsEntity entity, EcsWorld world)
        {
            OnSpawn(entity, world);
            _components.ForEach((component => component.EntityInit(entity, world, true)));
            Destroy(this);
        }

        public virtual void OnSpawn(EcsEntity entity, EcsWorld world)
        {
            this.gameObject.AddComponent<EntityRef>().Entity = entity;
            ref UnityView view = ref entity.Get<UnityView>();
            view.GameObject = this.gameObject;
            view.id = id;
            view.Transform = transform;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            id = gameObject.name;
        }

        [ContextMenu("Collect components")]
        private void CollectComponents()
        {
            _components = new List<ViewComponent>(transform.GetComponentsInChildren<ViewComponent>());
        }
#endif
    }
}