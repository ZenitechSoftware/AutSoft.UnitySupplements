using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class EditorSettingsExtensions
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider() =>
            new("Project/AutSoft/LicenseGenerator", SettingsScope.Project)
            {
                label = "License Generator",
                guiHandler = _ =>
                {
                    var s = LicenseGeneratorSettings.GetOrCreateSettings();
                    var settings = new SerializedObject(s);

                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._isIncludePackageLicensesEnabled)), new GUIContent("Include Package licenses"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._ignoredPackages)), new GUIContent("Ignored Packages", "Do not add licenses from the following Packages"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._includedLicensesFolderPath)), new GUIContent("Additional licenses folder"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._includedLicenseAssets)), new GUIContent("Additional license text assets"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._mergedLicenseAsset)), new GUIContent("Merged license asset"));
                    if (GUILayout.Button("Generate asset"))
                        EditorMenuExtensions.GenerateLicenseAsset();

                    settings.ApplyModifiedProperties();
                },
                keywords = new HashSet<string>(new[] { "License", "Generator" }),
            };
    }
}
