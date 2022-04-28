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

        private bool _isNotEmpty => _components != null;
        private bool _typeIsNotNull => _typeName != null;

        [PropertyOrder(-2)]
        [ShowIf("_isNotEmpty")]
        [ShowInInspector]
        [SerializeReference]
        [ListDrawerSettings(DraggableItems = false)]
        [ValueDropdown("GetTypes", IsUniqueList = true)]
        private List<object> _components = new List<object>();

        private List<ComponentInfoViewRuntime> _listComponents = new List<ComponentInfoViewRuntime>();

        private void Start()
        {
            Entity = WorldHandler.GetWorld().NewEntity();
            ref var view = ref Entity.Get<UnityViewComponent>();
            view.GameObject = gameObject;
            view.Transform = transform;

            foreach (var obj in _components)
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);
                action?.Invoke(Entity, obj);
            }

            _components.Clear();
            _components = null;

            IsNull = false;
        }

        private static object[] GetTypes
        {
            get
            {
                //var types = AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain);
                // var allTypes = AppDomain.CurrentDomain.GetAssemblies().GetAllTypes();

                var assembly = AppDomain.CurrentDomain.Load("Assembly-CSharp");
                var allTypes = assembly.GetAllTypes();

                object[] result = null;

                Type[] typesStruct =  allTypes.Where(type => type.IsValueType && !type.IsAbstract && type.Namespace == "Game").ToArray();

                result = new object[typesStruct.Length];

                int i = 0;
                foreach (var type in typesStruct)
                {
                    result[i] = Activator.CreateInstance(type);
                    i++;
                }

                return result;
            }
        }

        private List<string> GetTypeNames()
        {
            var types = GetTypes;

            List<string> result = new List<string>();

            foreach (var t in types)
            {
                result.Add(t.GetType().FullName);
            }

            if (Application.isPlaying)
            {
                var objOnEntity = ComponentsOnEntityInRuntime();

                foreach (var o in objOnEntity)
                {
                    if (result.Contains(o.GetType().FullName))
                    {
                        result.Remove(o.GetType().FullName);
                    }
                }
            }

            return result;
        }

        private List<object> ComponentsOnEntityInRuntime()
        {
            object[] objects = null;
            Entity.GetComponentValues(ref objects);

            return objects.ToList();
        }

        private void SetListInRuntime()
        {
            var objs = ComponentsOnEntityInRuntime();

            foreach (var obj in objs)
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(obj.GetType().FullName);

                _listComponents.Add(new ComponentInfoViewRuntime()
                {
                    Data = obj,
                    _entity = this.Entity,
                    _replaceAction = action
                });
            }
        }

        private void UpdateComponentsList()
        {
            if (Entity.IsAlive())
            {
                var action = EcsComponentTypesKeeper.GetReplaceAction(_typeName);
                var obj = Activator.CreateInstance(Type.GetType(_typeName));

                if (obj != null)
                    action?.Invoke(Entity, obj);

                _typeName = null;
            }
        }

        [HideIf("_isNotEmpty")]
        [ValueDropdown("GetTypeNames")]
        [PropertyOrder(0)]
        [ShowInInspector]
        [LabelText("Add Component")]
        [InlineButton("UpdateComponentsList", "+", ShowIf ="_typeIsNotNull")]
        private string _typeName = null;

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
        private List<ComponentInfoViewRuntime> ComponetsOnEntity
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
                }

                return _listComponents;
            }
            set { }
        }

        [Serializable]
        [InlineProperty]
        [HideReferenceObjectPicker]
        [HideLabel]
        public struct ComponentInfoViewRuntime
        {
            private object _component;
            internal Action<EcsEntity, object> _replaceAction;
            internal EcsEntity _entity;

            internal bool notUnityViewComponent => _component.GetType() != typeof(UnityViewComponent);

            [HideLabel]
            [DisplayAsString(false)]
            [ShowInInspector]
            [GUIColor(0, 1, 0)]
            internal string TypeName => _component.GetType().Name;

            [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
            [DisableContextMenu]
            [HideLabel]
            [ShowInInspector]
            [HideReferenceObjectPicker]
            [InlineButton("Del", "-", ShowIf = "notUnityViewComponent")]
            public object Data
            {
                get
                {
                    return _component;
                }
                set
                {
                    if (Application.isPlaying)
                    {
                        _component = value;

                        if (_component != null)
                        {
                            _replaceAction?.Invoke(this._entity, _component);
                        }
                    }
                }
            }

            private void Del()
            {
                _entity.Del(_component.GetType().FullName);
            }

            private static Color GetButtonColor()
            {
                Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
                return Color.HSVToRGB(Mathf.Cos((float)UnityEditor.EditorApplication.timeSinceStartup + 1f) * 0.225f + 0.325f, 1, 1);
            }
        }
    }
}
