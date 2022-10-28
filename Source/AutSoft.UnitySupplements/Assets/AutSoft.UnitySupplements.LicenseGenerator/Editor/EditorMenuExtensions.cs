#nullable enable
using Cysharp.Threading.Tasks;
using UnityEditor;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class EditorMenuExtensions
    {
        public static bool IsExecuting { get; private set; }

        [MenuItem("Tools / Generate License Asset")]
        public static void GenerateLicenseAsset()
        {
            IsExecuting = true;
            var settings = LicenseGeneratorSettings.GetOrCreateSettings();
            var context = new LicenseGeneratorContext(settings);
            LicenseGenerator.GenerateAsset(context)
                .ContinueWith(() => IsExecuting = false)
                .Forget((ex) =>
                {
                    IsExecuting = false;
                    context.Error($"An error occured while generating the license asset: {ex.Message}");
                });
        }

        [MenuItem("Tools / Generate License Asset", true)]
        public static bool IsEnabled() => !IsExecuting;
    }
}
