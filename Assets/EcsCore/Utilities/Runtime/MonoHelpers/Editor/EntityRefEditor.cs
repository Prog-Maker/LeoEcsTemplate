using Game;
using Leopotam.Ecs;
using Sirenix.OdinInspector.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityRef))]
public class EntityRefEditor : OdinEditor
{
    private EntityRef _entityRef;
    private int _typeIndex = 0;
    private Type[] _monoProviders;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _entityRef = (EntityRef)target;

        // DrawEntityInfo();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        DrawComponents();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        DrawAddPanel();
    }

    private void DrawEntityInfo()
    {
        EditorGUILayout.LabelField($"Entity Id {_entityRef.Entity.GetInternalId()}");
    }

    private void DrawAddPanel()
    {
        if(_monoProviders == null) _monoProviders = GetTypes();

        var componentNames = _monoProviders.Select(t => t.Name).ToArray();

        for (int i = 0; i < componentNames.Length; i++)
        {
            string name = componentNames[i];

            if (name.Contains("Provider"))
            {
                name = name.Replace("Provider", "");
            }

            componentNames[i] = name;
        }

        _typeIndex = EditorGUILayout.Popup(_typeIndex, componentNames);

        if (GUILayout.Button("Add component"))
        {
            var type = _monoProviders[_typeIndex];

            var typeToAdd = _entityRef.GetComponent(type);

            if (typeToAdd == null)
            {
               var component = _entityRef.gameObject.AddComponent(type);
               ((IConvertToEntity)component).Convert(_entityRef.Entity);
            }
        }
    }

    private void DrawComponents()
    {
        object[] list = null;

        if (_entityRef.Entity.IsAlive())
        {
            _entityRef.Entity.GetComponentValues(ref list);
        }

        if (list != null)
        {
            EditorGUILayout.BeginVertical();

            foreach (var component in list)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(component.GetType().Name);

                var nameType = component.GetType().FullName;

                if (GUILayout.Button("delete"))
                {
                    foreach (var c in _entityRef.gameObject.GetComponents<Component>())
                    {
                        if (c is IConvertToEntity)
                        {
                            var baseType = c.GetType().BaseType;

                            if (baseType.IsGenericType)
                            {
                                var args = baseType.GetGenericArguments()[0];
                                if(args.FullName == nameType)
                                {
                                    Destroy(c);
                                }
                            }
                        }
                    }
                    
                    _entityRef.Entity.Del(nameType);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }

    private Type[] GetTypes()
    {
        var types = AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain); 
        var allTypes = AppDomain.CurrentDomain.GetAssemblies().GetAllTypes();
        Type[] typesArray = allTypes.Where(type => InterfaceTypeExtension.ImplementsInterface<IConvertToEntity>(type) && !type.IsAbstract).ToArray();
        
        //Type[] typesStruct =  allTypes.Where(type => type.IsValueType && !type.IsAbstract && type.Namespace == "Game").ToArray();

        return typesArray;
    }
}
