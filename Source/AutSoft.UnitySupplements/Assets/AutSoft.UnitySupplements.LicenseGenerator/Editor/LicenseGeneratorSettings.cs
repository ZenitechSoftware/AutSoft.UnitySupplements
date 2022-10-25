#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public sealed class LicenseGeneratorSettings : ScriptableObject
    {
        private const string SettingsPath = "Assets/LicenseGeneratorSettings.asset";

        [Header("Input assets")]
        [SerializeField] internal bool _isIncludePackageLicensesEnabled;
        [SerializeField] internal List<string> _ignoredPackages = default!;
        [SerializeField] internal string _includedLicensesFolderPath = default!;
        [SerializeField] internal List<TextAsset> _includedLicenseAssets = default!;

        [Header("Output asset")]
        [SerializeField] internal TextAsset _mergedLicenseAsset = default!;

        [Header("Other")]
        [SerializeField] internal bool _logInfo;
        [SerializeField] internal bool _logError;

        public bool IsIncludePackageLicensesEnabled => _isIncludePackageLicensesEnabled;
        public IReadOnlyList<string> IgnoredPackages => _ignoredPackages;
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

            //set default settings
            settings._isIncludePackageLicensesEnabled = true;
            settings._logError = true;

            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();

            return settings;
        }
    }
}
