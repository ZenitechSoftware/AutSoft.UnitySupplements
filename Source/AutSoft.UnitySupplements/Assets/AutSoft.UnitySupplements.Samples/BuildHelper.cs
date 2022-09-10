#nullable enable
#if UNITY_EDITOR
using System.IO;
using System.Runtime.InteropServices;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples
{
    public static class BuildHelper
    {
        public static void RegenerateProjectFiles()
        {
            if (!RuntimeInformation.OSDescription.Contains("Windows")) return;

            const string vs2019 = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe";
            const string vs2022 = @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\devenv.exe";
            if (File.Exists(vs2019))
            {
                Debug.Log("Setting VS2022 as default");
                EditorPrefs.SetString("kScriptsDefaultApp", vs2019);
            }
            else if (File.Exists(vs2022))
            {
                Debug.Log("Setting VS2022 as default");
                EditorPrefs.SetString("kScriptsDefaultApp", vs2022);
            }
            else
            {
                Debug.LogError("Cloud not find Visual Studio installation");
            }

            EditorPrefs.SetInt("unity_project_generation_flag_h3448460154", 255);

            CodeEditor.Editor.CurrentCodeEditor.SyncAll();
        }
    }
}
#endif
