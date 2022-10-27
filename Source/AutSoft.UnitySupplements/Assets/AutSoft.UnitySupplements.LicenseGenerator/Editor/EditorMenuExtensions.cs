#nullable enable
using Cysharp.Threading.Tasks;
using UnityEditor;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class EditorMenuExtensions
    {
        [MenuItem("Tools / Generate License Asset")]
        public static void GenerateLicenseAsset()
        {
            var settings = LicenseGeneratorSettings.GetOrCreateSettings();
            var context = new LicenseGeneratorContext(settings);
            LicenseGenerator.GenerateAsset(context)
                .Forget((ex) => context.Error($"An error occured while generating the license asset: {ex.Message}"));
        }
    }
}
