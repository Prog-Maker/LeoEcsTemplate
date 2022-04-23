using Modules.Root;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class ECSMenus : MonoBehaviour
    {
        [MenuItem("GameObject/LeoECS/Create ECS Startup", false, 10)]
        static void CreateEcsStartup(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject go = new GameObject("ECSStartup");
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            
            go.AddComponent<EcsStartup>();
            
            Selection.activeObject = go;
        }
    }
}
