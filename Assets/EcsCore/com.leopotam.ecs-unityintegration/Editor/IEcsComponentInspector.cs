// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;

namespace Leopotam.Ecs.UnityIntegration.Editor
{
    /// <summary>
    /// Custom inspector for specified field type.
    /// </summary>
    public interface IEcsComponentInspector
    {
        /// <summary>
        /// Supported field type.
        /// </summary>
        Type GetFieldType();

        /// <summary>
        /// Renders provided instance of specified type.
        /// </summary>
        /// <param name="label">Label of field.</param>
        /// <param name="value">Value of field.</param>
        /// <param name="world">World instance.</param>
        /// <param name="entityId">Entity id.</param>
        // ReSharper disable once InconsistentNaming
        void OnGUI(string label, object value, EcsWorld world, ref EcsEntity entityId);
    }
}