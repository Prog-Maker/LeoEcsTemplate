using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "LeoECS_Editor/TemplateSettings")]
    public class TemplateSettings : ScriptableObject
    {
        public string ComponentsPath;
        public string ComponentProvidersPath;
    }
}
