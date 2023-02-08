#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    /// <summary>
    /// ScriptableObject that stores settings for <see cref="LicenseGenerator"/>.
    /// </summary>
    public sealed class LicenseGeneratorSettings : ScriptableObject
    {
        private const string DefaultSettingsPath = "Assets/LicenseGeneratorSettings.asset";

        [Header("Input assets")]
        [SerializeField] internal bool _isIncludePackageLicensesEnabled = true;
        [SerializeField] internal List<ManualLicenseAssignment> _assignments = new();
        [SerializeField] internal string _includedLicensesFolderPath = default!;
        [SerializeField] internal List<TextAsset> _includedLicenseAssets = new();

        [Header("Output asset")]
        [SerializeField] internal TextAsset _mergedLicenseAsset = default!;

        [Header("Other")]
        [SerializeField] internal bool _logInfo;
        [SerializeField] internal bool _logError = true;

        public bool IsIncludePackageLicensesEnabled => _isIncludePackageLicensesEnabled;
        public IReadOnlyList<ManualLicenseAssignment> Assignments => _assignments;
        public string IncludedLicensesFolderPath => _includedLicensesFolderPath;
        public IReadOnlyList<TextAsset> IncludedLicenseAssets => _includedLicenseAssets;
        public TextAsset MergedLicenseAsset => _mergedLicenseAsset;
        public bool LogInfo => _logInfo;
        public bool LogError => _logError;

        /// <summary>
        /// Finds or creates an asset in <see cref="AssetDatabase"/> of type <see cref="LicenseGeneratorSettings"/>.
        /// If no asset can been found, a new asset is created in the /Assets folder.
        /// </summary>
        public static LicenseGeneratorSettings GetOrCreateSettings()
        {
            var existingGuid = AssetDatabase.FindAssets($"t: {typeof(LicenseGeneratorSettings)}", new[] { "Assets" }).FirstOrDefault();
            if (existingGuid != null)
            {
                return AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>(AssetDatabase.GUIDToAssetPath(existingGuid));
            }
            var settings = CreateInstance<LicenseGeneratorSettings>();
            AssetDatabase.CreateAsset(settings, DefaultSettingsPath);
            AssetDatabase.SaveAssets();
            return settings;
        }

        /// <summary>
        /// Adds a new slot for manually assigned package licenses using the specified package name and empty assignment.
        /// If a slot with the given name already exists, no new item is added.
        /// </summary>
        public void AddAssignmentSlot(string packageName)
        {
            if (_assignments.Any(a => a.PackageName == packageName))
                return;
            _assignments.Add(new ManualLicenseAssignment(packageName, null));
            new SerializedObject(this).ApplyModifiedProperties();
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Sets the output asset of the generator that will be overwritten at the end of the process.
        /// </summary>
        public void SetOutputAsset(TextAsset asset)
        {
            _mergedLicenseAsset = asset;
            new SerializedObject(this).ApplyModifiedProperties();
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Utility class used to serialize manually assigned package licenses.
        /// </summary>
        [Serializable]
        public class ManualLicenseAssignment
        {
            [SerializeField] internal string _packageName = default!;
            [SerializeField] internal TextAsset? _licenseAsset;

            [Obsolete("Used by serializer")]
            public ManualLicenseAssignment() { }
            public ManualLicenseAssignment(string packageName, TextAsset? licenseAsset)
            {
                _packageName = packageName;
                _licenseAsset = licenseAsset;
            }

            public string PackageName => _packageName;
            public TextAsset? LicenseAsset => _licenseAsset;
        }
    }
}
