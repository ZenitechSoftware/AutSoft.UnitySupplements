#nullable enable
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public sealed class LicenseGeneratorSettings : ScriptableObject
    {
        private const string SettingsPath = "Assets/LicenseGeneratorSettings.asset";

        [Header("Input sources")]
        [SerializeField] internal bool _isIncludePackageLicensesEnabled;
        [SerializeField] internal string _includedLicensesFolderPath = default!;
        [SerializeField] internal List<TextAsset> _includedLicenseAssets = default!;

        [Header("Output assets")]
        [SerializeField] internal bool _isGenerateMergedFileEnabled;
        [SerializeField] internal string _mergedFilePath = default!;
        [SerializeField] internal bool _isGenerateIndividualFilesEnabled;
        [SerializeField] internal string _individualFilesFolderPath = default!;

        [Header("Other")]
        [SerializeField] internal bool _logInfo;
        [SerializeField] internal bool _logError;

        public bool IsIncludePackageLicensesEnabled => _isIncludePackageLicensesEnabled;
        public string IncludedLicensesFolderPath => _includedLicensesFolderPath;
        public IReadOnlyList<TextAsset> IncludedLicenseAssets => _includedLicenseAssets;
        public bool IsGenerateMergedFileEnabled => _isGenerateMergedFileEnabled;
        public string MergedFilePath => _mergedFilePath;
        public bool IsGenerateIndividualFilesEnabled => _isGenerateIndividualFilesEnabled;
        public string IndividualFilesFolderPath => _individualFilesFolderPath;
        public bool LogInfo => _logInfo;
        public bool LogError => _logError;

        public static LicenseGeneratorSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>(SettingsPath);
            if (settings != null) return settings;

            settings = CreateInstance<LicenseGeneratorSettings>();

            //set default settings
            settings._isIncludePackageLicensesEnabled = true;
            settings._isGenerateMergedFileEnabled = true;
            settings._mergedFilePath = "Licenses/Third-Party-Licenses.txt";
            settings._logError = true;

            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();

            return settings;
        }
    }
}
