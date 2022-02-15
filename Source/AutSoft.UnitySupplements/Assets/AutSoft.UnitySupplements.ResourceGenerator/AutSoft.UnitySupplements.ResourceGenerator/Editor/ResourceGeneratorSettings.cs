using AutSoft.UnitySupplements.ResourceGenerator.Editor.Extensions;
using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor
{
    public sealed class ResourceGeneratorSettings : ScriptableObject
    {
        [Serializable]
        public sealed class ResourceData : IResourceData
        {
            [SerializeField] private string _className = default;
            [SerializeField] private string[] _fileExtensions = default;
            [SerializeField] private string _dataType = default;

            public ResourceData()
            {
            }

            public ResourceData(string className, string[] fileExtensions, string dataType)
            {
                _className = className;
                _fileExtensions = fileExtensions;
                _dataType = dataType;
            }

            public string ClassName => _className;
            public IReadOnlyList<string> FileExtensions => _fileExtensions;
            public string DataType => _dataType;
        }

        private const string SettingsPath = "Assets/ResourceGenerator.asset";

        [Header("General settings")]
        [SerializeField] private string _baseNamespace;
        [SerializeField] private string _className;

        [SerializeField]
        [Tooltip("Relative path from the Assets folder")]
        private string _folderPath;

        [SerializeField] private bool _logInfo;
        [SerializeField] private bool _logError;
        [Header("Resources")]
        [SerializeField] private List<string> _usings;
        [SerializeField] private List<ResourceData> _data;
        [Header("Layers")]
        [SerializeField] private bool _generateLayers;
        [Header("Scene buttons")]
        [SerializeField] private bool _generateSceneButtons;
        [SerializeField] private List<string> _sceneNames;

        public string FolderPath => _folderPath;
        public string BaseNamespace => _baseNamespace;
        public string ClassName => _className;
        public bool LogInfo => _logInfo;
        public bool LogError => _logError;
        public bool GenerateLayers => _generateLayers;
        public bool GenerateSceneButtons => _generateSceneButtons;
        public IReadOnlyList<string> SceneNames => _sceneNames;
        public IReadOnlyList<string> Usings => _usings;
        public IReadOnlyList<ResourceData> Data => _data;

        public static ResourceGeneratorSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<ResourceGeneratorSettings>(SettingsPath);
            if (settings != null) return settings;

            settings = CreateInstance<ResourceGeneratorSettings>();

            settings._folderPath = string.Empty;
            settings._baseNamespace = string.Empty;
            settings._className = "ResourcePaths";
            settings._logInfo = false;
            settings._logError = true;

            var (data, usings) = CreateDefaultFileMappings();

            settings._data = data;
            settings._usings = usings;

            settings._generateLayers = true;

            settings._generateSceneButtons = true;
            settings._sceneNames = new();

            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();

            return settings;
        }

        private static (List<ResourceData> data, List<string> usings) CreateDefaultFileMappings() =>
            // https://docs.unity3d.com/Manual/BuiltInImporters.html
            (
                new List<ResourceData>
                {
                    new ResourceData("Scenes", new[] { "*.unity" }, "Scene"),
                    new ResourceData("Prefabs", new[] { "*.prefab" }, "GameObject"),
                    new ResourceData("Materials", new[] { "*.mat" }, "Material"),
                    new ResourceData("AudioClips", new[] { "*.ogg", "*.aif", "*.aiff", "*.flac", "*.mp3", "*.mod", "*.it", "*.s3m", "*.xm", "*.wav" }, "AudioClip"),
                    new ResourceData("Sprites", new[] { "*.jpg", "*.jpeg", "*.tif", "*.tiff", "*.tga", "*.gif", "*.png", "*.psd", "*.bmp", "*.iff", "*.pict", "*.pic", "*.pct", "*.exr", "*.hdr" }, "Sprite"),
                    new ResourceData("TextAssets", new[] { "*.txt", "*.html", "*.htm", "*.xml", "*.bytes", "*.json", "*.csv", "*.yaml", "*.fnt" }, "TextAsset"),
                    new ResourceData("Fonts", new[] { "*.ttf", "*.dfont", "*.otf", "*.ttc" }, "Font")
                },
                new List<string>
                {
                    "UnityEngine",
                    "UnityEngine.SceneManagement",
                }
            );

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider() =>
            new("Project/ResourceGenerator", SettingsScope.Project)
            {
                label = "ResourceGenerator",
                guiHandler = _ =>
                {
                    var settings = new SerializedObject(GetOrCreateSettings());

                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_folderPath)), new GUIContent("Folder from Assets"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_baseNamespace)), new GUIContent("Namespace"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_className)), new GUIContent("Class name"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_logInfo)), new GUIContent("Log Infos"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_logError)), new GUIContent("Log Errors"));

                    if (GUILayout.Button("Reset file mappings"))
                    {
                        var (data, usings) = CreateDefaultFileMappings();
                        settings.FindProperty(nameof(_data)).SetValue(data);
                        settings.FindProperty(nameof(_usings)).SetValue(usings);
                    }

                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_usings)), new GUIContent("Using directives"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_data)), new GUIContent("Data"));

                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_generateLayers)), new GUIContent("Generate Layers"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_generateSceneButtons)), new GUIContent("Generate Scene Buttons"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(_sceneNames)), new GUIContent("Scene names"));

                    settings.ApplyModifiedProperties();
                },
                keywords = new HashSet<string>(new[] { "Resource", "Path" }),
            };
    }
}
