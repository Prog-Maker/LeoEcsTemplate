using Leopotam.Ecs2.UnityIntegration.Editor.Prototypes;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class ComponentCreator : EditorWindow
    {
        private static TemplateSettings TemplateSettings;

        private static ComponentCreatorSettings _settings;

        private string _componentName;
        private bool _createProvider = true;

        private string _componentsPath = "Assets/Code/LeoEcs/Components/";
        private string _componentProvidersPath = "Assets/Code/LeoEcs/UnityComponents/";

        private string _componentText = "";
        private string _componentProviderText = "";

        const string _componentTemplate = "Component.cs.txt";
        const string _componentProviderTemplate = "ComponentProvider.cs.txt";

        [MenuItem("Tools/LeoECS/Component Creator", false, -199)]
        private static void Open()
        {
            ComponentCreator componentCreator = GetWindow<ComponentCreator>("Component Creator");
        }

        private void OnGUI()
        {
            _settings = (ComponentCreatorSettings)EditorGUIUtility.Load("ComponentCreatorSettings.asset");
            TemplateSettings = _settings.TemplateSettings;

            if (TemplateSettings == null)
            {
                DrawGetSetting();
                return;
            }
            
            DrawPaths();
            DrawCreateMenu();
        }

        private void DrawPaths()
        {
            _componentsPath = TemplateSettings.ComponentsPath;
            _componentProvidersPath = TemplateSettings.ComponentProvidersPath;

            _componentsPath = EditorGUILayout.TextField("Components Path", _componentsPath);
            _componentProvidersPath = EditorGUILayout.TextField("Component Providers Path", _componentProvidersPath);

            EditorGUILayout.Space();
        }

        private void DrawGetSetting()
        {
            TemplateSettings = (TemplateSettings)EditorGUILayout.ObjectField(TemplateSettings, typeof(TemplateSettings), true);
            _settings.TemplateSettings = TemplateSettings;
            EditorUtility.SetDirty(_settings);
        }

        private void DrawCreateMenu()
        {
            EditorGUILayout.LabelField("Component");
            _componentName = EditorGUILayout.TextField("Name", _componentName);
            _createProvider = EditorGUILayout.Toggle("Create provider", _createProvider);

            if (GUILayout.Button("Create"))
            {
                var componentContent = GetTemplateContent(_componentTemplate);
                var providerContent = GetTemplateContent(_componentProviderTemplate);

                _componentText = CreateTemplate(componentContent, _componentName);
                _componentProviderText = CreateTemplate(providerContent, _componentName);

                CreateComponent();

                _componentName = "";
            }
        }

        private void CreateComponent()
        {
            File.WriteAllText(_componentsPath + _componentName + ".cs", _componentText);
            
            if(_createProvider) 
            File.WriteAllText(_componentProvidersPath + _componentName + "Provider.cs", _componentProviderText);

            AssetDatabase.Refresh();
        }

        static string GetTemplateContent(string proto)
        {
            // hack: its only one way to get current editor script path. :(
            var pathHelper = CreateInstance<TemplateGenerator> ();
            var path = Path.GetDirectoryName (AssetDatabase.GetAssetPath (MonoScript.FromScriptableObject (pathHelper)));
            DestroyImmediate(pathHelper);
            try
            {
                return File.ReadAllText(Path.Combine(path ?? "", proto));
            }
            catch
            {
                return null;
            }
        }

        public static string CreateTemplate(string proto, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "Invalid filename";
            }
            var ns = EditorSettings.projectGenerationRootNamespace.Trim ();
            if (string.IsNullOrEmpty(EditorSettings.projectGenerationRootNamespace))
            {
                ns = "Client";
            }
            proto = proto.Replace("#NS#", ns);
            proto = proto.Replace("#SCRIPTNAME#", SanitizeClassName(fileName));

            return proto;
        }

        static string SanitizeClassName(string className)
        {
            var sb = new System.Text.StringBuilder ();
            var needUp = true;
            foreach (var c in className)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(needUp ? char.ToUpperInvariant(c) : c);
                    needUp = false;
                }
                else
                {
                    needUp = true;
                }
            }
            return sb.ToString();
        }
    }
}
