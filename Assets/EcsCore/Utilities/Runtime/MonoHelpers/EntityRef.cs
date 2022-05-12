using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Leopotam.Ecs
{
    public class EntityRef : MonoBehaviour
    {
        [HideInInspector] public EcsEntity Entity;
        [HideInInspector] public bool IsNull = true;

        private void OnEnable()
        {
            if (!IsNull)
            {
                Entity = WorldHandler.GetWorld().NewEntity();
                ref var view = ref Entity.Get<UnityView>();
                view.GameObject = gameObject;
                view.Transform = transform;

                return;
            }

            IsNull = true;
        }

#if UNITY_EDITOR && ODIN_INSPECTOR

        private object[] objectsCashed;

        private List<ComponentInfoView> _listComponents = new List<ComponentInfoView>();

        private List<object> ComponentsOnEntity()
        {
            object[] objects = null;
            Entity.GetComponentValues(ref objects);

            objectsCashed = objects;

            return objects.ToList();
        }

        private void SetList()
        {
            var objs = ComponentsOnEntity();

            foreach (var obj in objs)
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);

                _listComponents.Add(new ComponentInfoView()
                {
                    Data = obj,
                    _entity = this.Entity,
                    _replaceAction = action
                });
            }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-2)]
        private string EntityId
        {
            get { return Entity.GetInternalId().ToString(); }
        }


        [DisableContextMenu]
        [PropertySpace]
        [ShowInInspector]
        [HideReferenceObjectPicker]
        [PropertyOrder(-1)]
        [ListDrawerSettings(DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
        private List<ComponentInfoView> ComponetsOnEntity
        {
            get
            {
                if (Application.isPlaying && Entity.IsAlive())
                {
                    _listComponents.Clear();
                    SetList();
                }
                return _listComponents;
            }
            set { }
        }


        [System.Serializable]
        [InlineProperty]
        [HideReferenceObjectPicker]
        [HideLabel]
        public struct ComponentInfoView
        {
            private object _component;
            internal System.Action<EcsEntity, object> _replaceAction;
            internal EcsEntity _entity;

            [HideLabel]
            [DisplayAsString(false)]
            [ShowInInspector]
            internal string TypeName => _component.GetType().Name;

            [DisableContextMenu]
            [HideLabel]
            [ShowInInspector]
            [HideReferenceObjectPicker]
            public object Data
            {
                get { return _component; }
                set
                {
                    _component = value;

                    if (_component != null)
                    {
                        _replaceAction?.Invoke(this._entity, _component);
                    }
                }
            }
        }
#endif
    }


    [System.Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    [HideLabel]
    internal class EntityInfoViewer
    {
        private EcsEntity _entity;

        private List<ComponentInfoView> _listComponents = new List<ComponentInfoView>();

        public EntityInfoViewer(EcsEntity ecsEntity)
        {
            _entity = ecsEntity;
        }

        private List<object> ComponentsOnEntity()
        {
            object[] objects = null;
            _entity.GetComponentValues(ref objects);

            return objects.ToList();
        }

        private void SetList()
        {
            var objs = ComponentsOnEntity();

            foreach (var obj in objs)
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);

                _listComponents.Add(new ComponentInfoView()
                {
                    Data = obj,
                    _entity = this._entity,
                    _replaceAction = action
                });
            }
        }

        [DisableContextMenu]
        [PropertySpace]
        [ShowInInspector]
        [HideReferenceObjectPickerAttribute]
        [ListDrawerSettings(DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
        private List<ComponentInfoView> ComponetsOnEntity
        {
            get
            {
                _listComponents.Clear();
                SetList();
                return _listComponents;
            }
            set { }
        }


        [System.Serializable]
        [InlineProperty]
        [HideReferenceObjectPicker]
        [HideLabel]
        public struct ComponentInfoView
        {
            private object _component;
            internal System.Action<EcsEntity, object> _replaceAction;
            internal EcsEntity _entity;

            [HideLabel]
            [DisplayAsString(false)]
            [ShowInInspector]
            internal string TypeName => _component.GetType().Name;

            [DisableContextMenu]
            [LabelText("$" + nameof(TypeName))]
            [ShowInInspector]
            [HideReferenceObjectPicker]
            public object Data
            {
                get { return _component; }
                set
                {
                    _component = value;

                    if (_component != null)
                    {
                        _replaceAction?.Invoke(this._entity, _component);
                    }
                }
            }
        }
    }

    [DisableContextMenu]
    [System.Serializable]
    internal class EntityInfoView
    {
        [ReadOnly]
        public int ID;

        private EcsEntity _entity;

        [ShowInInspector]
        internal EntityInfoViewer _entityInfoViewer;

        public EntityInfoView(EcsEntity ecsEntity)
        {
            _entity = ecsEntity;
            _entityInfoViewer = new EntityInfoViewer(_entity);
            ID = _entity.GetInternalId();
        }
    }
}