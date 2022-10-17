#nullable enable
using System;
using System.Reflection;
using UnityEditor;

namespace AutSoft.UnitySupplements.ResourceGenerator.Sample
{
    public static class BuildHelper
    {
        public static void RegenerateProjectFiles()
        {
            EditorPrefs.SetString("kScriptsDefaultApp", @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe");
            EditorPrefs.SetInt("unity_project_generation_flag_h3448460154", 255);

            var t = Type.GetType("UnityEditor.SyncVS,UnityEditor") ?? throw new InvalidOperationException("Cloud not get editor type");
            var syncSolution = t.GetMethod("SyncSolution", BindingFlags.Public | BindingFlags.Static) ?? throw new InvalidOperationException("Cloud not get editor sync method");
            syncSolution.Invoke(null, null);
        }
    }
}
