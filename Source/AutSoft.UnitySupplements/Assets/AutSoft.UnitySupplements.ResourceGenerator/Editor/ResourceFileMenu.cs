#nullable enable
using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor
{
    /// <summary>
    /// UnityEditor extension to add Resource Generator function to the "Tools" menu.
    /// </summary>
    public static class ResourceFileMenu
    {
        [MenuItem("Tools / Generate Resources Paths")]
        public static void GenerateResources()
        {
            if (EditorUtility.DisplayCancelableProgressBar("Resource Generator", "Initializing context...", 0.0f)) throw new OperationCanceledException();

            var settings = ResourceGeneratorSettings.GetOrCreateSettings();
            var assetsFolder = Path.GetFullPath(Application.dataPath);
            var context = new ResourceContext
            (
                assetsFolder,
                settings.FolderPath,
                settings.BaseNamespace,
                settings.ClassName,
                settings.LogInfo ? Debug.Log : LogEmpty,
                settings.LogError ? Debug.LogError : LogEmpty,
                settings.Data,
                settings.Usings,
                settings.GenerateLayers,
                settings.GenerateSceneButtons,
                settings.SceneNames,
                settings.EditorPrefsData
            );

            if (EditorUtility.DisplayCancelableProgressBar("Resource Generator", "Genreating content...", 0.25f)) throw new OperationCanceledException();
            context.Info("Resource Path generation started");
            var fileContent = ResourceFileGenerator.CreateResourceFile(context);

            if (EditorUtility.DisplayCancelableProgressBar("Resource Generator", $"Checking {context.ClassName}.cs...", 0.5f)) throw new OperationCanceledException();
            var filePath = Path.GetFullPath(Path.Combine(context.AssetsFolder, context.FolderPath, $"{context.ClassName}.cs"));
            if (File.Exists(filePath))
            {
                var old = File.ReadAllText(filePath);
                if (old == fileContent)
                {
                    EditorUtility.ClearProgressBar();
                    context.Info("Resource file did not change");
                    return;
                }
            }

            EditorUtility.DisplayProgressBar("Resource Generator", $"Writing {context.ClassName}.cs...", 0.75f);

            File.WriteAllText(filePath, fileContent);

            EditorUtility.ClearProgressBar();
            context.Info($"Created resource file at: {filePath}");
        }

        private static Action<string> LogEmpty => _ => { };
    }
}
