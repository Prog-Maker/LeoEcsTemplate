using System;
using UnityEditor;

namespace Leopotam.Ecs.UnityIntegration.Editor.Inspectors
{
    public sealed class Float : IEcsComponentInspector
    {
        Type IEcsComponentInspector.GetFieldType()
        {
            return typeof(float);
        }

        void IEcsComponentInspector.OnGUI(string label, object value, EcsWorld world, ref EcsEntity entityId)
        {
            value = EditorGUILayout.FloatField(label, (float)value);
        }
    }
}
