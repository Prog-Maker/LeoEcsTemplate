using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "LeoECS_Editor/ComponentCreatorSettings")]
    public class ComponentCreatorSettings : ScriptableObject
    {
        public string PathForSettingTemplate;
        public TemplateSettings TemplateSettings;
    }
}