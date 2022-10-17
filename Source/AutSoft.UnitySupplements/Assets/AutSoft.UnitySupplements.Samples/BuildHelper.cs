#nullable enable
using AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples;
using System.IO;
using System.Runtime.InteropServices;
using Unity.CodeEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples
{
    public static class BuildHelper
    {
        public static void RegenerateProjectFiles()
        {
            if (!RuntimeInformation.OSDescription.Contains("Windows")) return;

            const string vs2022 = @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\devenv.exe";
            if (File.Exists(vs2022))
            {
                Debug.Log("Setting VS2022 as default");
                ResourcePaths.KnownEditorPrefs.CodeEditor.SetkScriptsDefaultApp(vs2022);
            }
            else
            {
                Debug.LogError("Cloud not find Visual Studio installation");
            }

            ResourcePaths.KnownEditorPrefs.CodeEditor.Setunity_project_generation_flag(255);

            CodeEditor.Editor.CurrentCodeEditor.SyncAll();
        }
    }
}
