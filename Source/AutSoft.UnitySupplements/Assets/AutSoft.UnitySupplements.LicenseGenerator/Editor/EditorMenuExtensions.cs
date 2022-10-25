#nullable enable
using System;
using UnityEditor;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class EditorMenuExtensions
    {
        [MenuItem("Tools / Generate License Assets")]
        public static void GenerateLicenseAssets()
        {
            var settings = LicenseGeneratorSettings.GetOrCreateSettings();
            settings._logInfo = true;//debug
            var context = new LicenseGeneratorContext(settings);

            context.Info("License assets generation started");
            new LicenseGenerator().GenerateAssets(context);
        }
    }
}
