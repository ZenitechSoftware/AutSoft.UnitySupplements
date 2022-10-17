#nullable enable
using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules;
using System;
using System.Collections.Generic;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation
{
    /// <summary>
    /// User defined settings and data and loggers
    /// </summary>
    public sealed class ResourceContext
    {
        public ResourceContext
        (
            string assetsFolder,
            string folderPath,
            string baseNamespace,
            string className,
            Action<string> info,
            Action<string> error,
            IReadOnlyList<IResourceData> data,
            IReadOnlyList<string> usings,
            bool generateLayers,
            bool generateSceneButtons,
            IReadOnlyList<string> sceneNames,
            EditorPrefsData editorPrefsData)
        {
            AssetsFolder = assetsFolder;
            FolderPath = folderPath;
            BaseNamespace = baseNamespace;
            ClassName = className;
            Info = info;
            Error = error;
            GenerateLayers = generateLayers;
            GenerateSceneButtons = generateSceneButtons;
            SceneNames = sceneNames;
            EditorPrefsData = editorPrefsData;
            Data = data;
            Usings = usings;
        }

        /// <summary>
        /// Full path to the Unity Assets folder
        /// </summary>
        public string AssetsFolder { get; }

        /// <summary>
        /// User defined path to the desired relative folder from the <see cref="AssetsFolder"/>
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// User defined namespace of the generated file
        /// </summary>
        public string BaseNamespace { get; }

        /// <summary>
        /// User defined class name of the generated file
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Info level logger
        /// </summary>
        public Action<string> Info { get; }

        /// <summary>
        /// Error level logger
        /// </summary>
        public Action<string> Error { get; }

        /// <summary>
        /// Data used by <see cref="AllResources"/>
        /// </summary>
        public IReadOnlyList<IResourceData> Data { get; }

        /// <summary>
        /// User defined custom usings
        /// </summary>
        public IReadOnlyList<string> Usings { get; }

        /// <summary>
        /// Generate layers paths and masks
        /// </summary>
        public bool GenerateLayers { get; }

        /// <summary>
        /// Generate buttons to load scenes
        /// </summary>
        public bool GenerateSceneButtons { get; }

        /// <summary>
        /// Names of scenes to generate load button for
        /// </summary>
        public IReadOnlyList<string> SceneNames { get; }

        /// <summary>
        /// Generation data of Editor Preferences
        /// </summary>
        public EditorPrefsData EditorPrefsData { get; }
    }
}
