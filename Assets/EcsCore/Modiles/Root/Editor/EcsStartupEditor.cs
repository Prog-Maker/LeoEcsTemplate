#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UICoreECS.Editor;
using UnityEditor;
using UnityEditorInternal;

namespace Modules.Root 
{
    [CustomEditor(typeof(EcsStartup), true)]
    public class EcsStartupEditor : OdinEditor
    {
        //private ReorderableList _providersList;

        //private void OnEnable()
        //{
        //    _providersList = EditorHelper.DrawReorderableList(serializedObject, "_systemProviders", "Providers");
        //}

        //public override void OnInspectorGUI()
        //{
        //    DrawDefaultInspector();
            
        //    EditorGUILayout.Space();
            
        //    _providersList.DoLayoutList();
            
        //    serializedObject.ApplyModifiedProperties();
        //}
    }
}
#endif
