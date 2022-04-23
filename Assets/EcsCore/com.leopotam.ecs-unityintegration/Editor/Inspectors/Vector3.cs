// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Leopotam.Ecs.UnityIntegration.Editor.Inspectors
{
    sealed class Vector3Inspector : IEcsComponentInspector
    {
        Type IEcsComponentInspector.GetFieldType()
        {
            return typeof(Vector3);
        }

        void IEcsComponentInspector.OnGUI(string label, object value, EcsWorld world, ref EcsEntity entityId)
        {
            value = EditorGUILayout.Vector3Field(label, (Vector3)value);
        }
    }
}