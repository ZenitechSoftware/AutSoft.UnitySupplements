#nullable enable
using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor
{
    public static class ResourceFileMenu
    {
        [MenuItem("Tools / Generate Resources Paths")]
        public static void GenerateResources()
        {
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
                settings.SceneNames

            );

            context.Info("Resource Path generation started");
            var fileContent = ResourceFileGenerator.CreateResourceFile(context);

            var filePath = Path.GetFullPath(Path.Combine(context.AssetsFolder, context.FolderPath, $"{context.ClassName}.cs"));

            if (File.Exists(filePath))
            {
                var old = File.ReadAllText(filePath);
                if (old == fileContent)
                {
                    context.Info("Resource file did not change");
                    return;
                }
            }

            File.WriteAllText(filePath, fileContent);
            context.Info($"Created resource file at: {filePath}");
        }

        private static Action<string> LogEmpty => _ => { };
    }
}
