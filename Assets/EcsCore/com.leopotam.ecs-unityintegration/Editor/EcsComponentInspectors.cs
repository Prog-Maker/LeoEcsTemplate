// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.Ecs.UnityIntegration.Editor
{
    static class EcsComponentInspectors
    {
        static readonly Dictionary<Type, IEcsComponentInspector> Inspectors = new Dictionary<Type, IEcsComponentInspector> ();

        static EcsComponentInspectors()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IEcsComponentInspector).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        if (Activator.CreateInstance(type) is IEcsComponentInspector inspector)
                        {
                            var componentType = inspector.GetFieldType ();
                           
                            if (Inspectors.ContainsKey(componentType))
                            {
                                Debug.LogWarningFormat("Inspector for \"{0}\" already exists, new inspector will be used instead.", componentType.Name);
                            }
                            
                            Inspectors[componentType] = inspector;
                        }
                    }
                }
            }
        }

        public static bool Render(string label, Type type, object value, EcsEntityObserver observer)
        {
            if (Inspectors.TryGetValue(type, out var inspector))
            {
                inspector.OnGUI(label, value, observer.World, ref observer.Entity);
                return true;
            }
            return false;
        }
    }
}