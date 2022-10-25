#nullable enable
using UnityEditor;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class EditorMenuExtensions
    {
        [MenuItem("Tools / Generate License Asset")]
        public static void GenerateLicenseAsset()
        {
            var settings = LicenseGeneratorSettings.GetOrCreateSettings();
            settings._logInfo = true;//debug
            var context = new LicenseGeneratorContext(settings);

            context.Info("License asset generation started");
            new LicenseGenerator().GenerateAsset(context);
        }
    }
}
