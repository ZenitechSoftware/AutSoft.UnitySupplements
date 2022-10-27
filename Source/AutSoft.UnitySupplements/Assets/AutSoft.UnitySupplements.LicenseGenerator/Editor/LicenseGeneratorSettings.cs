#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public sealed class LicenseGeneratorSettings : ScriptableObject
    {
        private const string SettingsPath = "Assets/LicenseGeneratorSettings.asset";

        [Header("Input assets")]
        [SerializeField] internal bool _isIncludePackageLicensesEnabled = true;
        [SerializeField] internal List<ManualLicenseAssignment> _assignments = new();// = default!;
        [SerializeField] internal string _includedLicensesFolderPath = default!;
        [SerializeField] internal List<TextAsset> _includedLicenseAssets = new();// = default!;

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

        public static LicenseGeneratorSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>(SettingsPath);
            if (settings != null) return settings;
            settings = CreateInstance<LicenseGeneratorSettings>();
            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();
            return settings;
        }

        public void AddAssignmentSlot(string packageName)
        {
            if (_assignments.Any(a => a.PackageName == packageName))
                return;
            _assignments.Add(new ManualLicenseAssignment(packageName, null));
            new SerializedObject(this).ApplyModifiedProperties();
        }

        [Serializable]
        public class ManualLicenseAssignment
        {
            [SerializeField] internal string _packageName = default!;
            [SerializeField] internal TextAsset? _licenseAsset = default!;

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
