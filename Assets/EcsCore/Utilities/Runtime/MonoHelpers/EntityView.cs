using Leopotam.Ecs;
using System;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

namespace Game
{
    public class EntityView : MonoBehaviour
    {
        [HideInInspector] public EcsEntity Entity;
        [HideInInspector] public bool IsNull = true;

        public EcsEntity InnerEntity;
        public EcsWorld World;

        [PropertyOrder(-2)]
        [ValueDropdown("GetTypes", IsUniqueList = true)]
        public List<string> Components;

        [ShowInInspector]
        private List<object> _components = new List<object>();

        private List<ComponentInfoView> _listComponents = new List<ComponentInfoView>();

        private void Start()
        {
            Entity = WorldHandler.GetWorld().NewEntity();
            ref var view = ref Entity.Get<UnityViewComponent>();
            view.GameObject = gameObject;
            view.Transform = transform;

            foreach (var c in Components)
            {
                var obj = Activator.CreateInstance(Type.GetType(c));
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);
                action?.Invoke(Entity, obj);
            }
            _components = ComponentsOnEntityInEditor();

            IsNull = false;
        }

        private static string[] GetTypes
        {
            get
            {
                var types = AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain);
                var allTypes = AppDomain.CurrentDomain.GetAssemblies().GetAllTypes();
                string[] strings = null;

                Type[] typesStruct =  allTypes.Where(type => type.IsValueType && !type.IsAbstract && type.Namespace == "Game").ToArray();

                //return typesStruct;
                strings = new string[typesStruct.Length];

                for (int i = 0; i < typesStruct.Length; i++)
                {
                    strings[i] = typesStruct[i].FullName;
                }

                return strings;
            }
        }

        private List<object> ComponentsOnEntityInRuntime()
        {
            object[] objects = null;
            Entity.GetComponentValues(ref objects);

            return objects.ToList();
        }

        private List<object> ComponentsOnEntityInEditor()
        {
            object[] objects = null;
            InnerEntity.GetComponentValues(ref objects);

            return objects.ToList();
        }

        private void SetListInRuntime()
        {
            var objs = ComponentsOnEntityInRuntime();

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

        private void SetListInEditor()
        {
            var objs = ComponentsOnEntityInEditor();

            foreach (var obj in objs)
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);

                _listComponents.Add(new ComponentInfoView()
                {
                    Data = obj,
                    _entity = this.InnerEntity,
                    _replaceAction = action
                });
            }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-3)]
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
                    SetListInRuntime();
                }
                else
                {
                    _listComponents.Clear();
                    SetListInEditor();
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
                get
                {
                    return _component;
                }
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

    [CustomEditor(typeof(EntityView))]
    public class EntityViewEditor : OdinEditor
    {
        private EntityView _entityView;
        private EcsWorld _world;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        protected override void OnEnable()
        {
            _entityView = (EntityView)target;

            Debug.Log("Entity is Alive - " + _entityView.InnerEntity.IsAlive());
            Debug.Log("World not null - " + _entityView.World != null);

            if (_entityView.World == null)
            {
                var sceneWorld = FindObjectOfType<SceneWorld>();
                if (sceneWorld)
                {
                    _entityView.World = sceneWorld.EditorWorld;
                }
                else
                {
                    sceneWorld = new GameObject().AddComponent<SceneWorld>();
                    sceneWorld.EditorWorld = new EcsWorld();
                    _entityView.World = sceneWorld.EditorWorld;
                }
            }

            if (!_entityView.InnerEntity.IsAlive())
            {
                _entityView.InnerEntity = _entityView.World.NewEntity();
                ref var view =  ref _entityView.InnerEntity.Get<UnityViewComponent>();
                view.GameObject = _entityView.gameObject;
                view.Transform = _entityView.transform;
            }
        }
    }

    public static class EntityViewDataKeeper
    {
        public static EcsWorld EditorWorld;

        static EntityViewDataKeeper()
        {
            EditorWorld = new EcsWorld();
        }
    }
}
