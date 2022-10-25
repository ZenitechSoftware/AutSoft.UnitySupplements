using AutSoft.UnitySupplements.LicenseGenerator.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._includedLicensesFolderPath)), new GUIContent("Additional licenses folder"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._includedLicenseAssets)), new GUIContent("Additional license text assets"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._isGenerateMergedFileEnabled)), new GUIContent("Generate merged file"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._mergedFilePath)), new GUIContent("Merged file path"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._isGenerateIndividualFilesEnabled)), new GUIContent("Generate individual files"));
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(s._individualFilesFolderPath)), new GUIContent("Individual files folder"));

                    //if (GUILayout.Button("Reset known Editor preferences"))
                    //{
                    //    var preferences = CreateDefaultPreferences();
                    //    settings.FindProperty(nameof(s._preferences)).SetValue(preferences);
                    //}

                    settings.ApplyModifiedProperties();
                },
                keywords = new HashSet<string>(new[] { "License", "Generator" }),
            };
    }
}
