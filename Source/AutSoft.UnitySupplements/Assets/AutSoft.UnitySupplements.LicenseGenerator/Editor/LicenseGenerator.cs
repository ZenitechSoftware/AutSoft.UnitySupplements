#nullable enable
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public static class LicenseGenerator
    {
        private const string DefaultOutputAssetPath = "Assets/Licenses.txt";
        private static string LicenseSeparator =>
            Environment.NewLine + "+ + + + + + + + + + + + + + + +" +
            Environment.NewLine + Environment.NewLine;

        /// <summary>
        /// Generates a merged license asset from existing licenses used in the project.
        /// See the <see cref="LicenseGeneratorSettings"/> asset to configure.
        /// </summary>
        /// <param name="ctx">A generator context created from a <see cref="LicenseGeneratorSettings"/> asset.</param>
        public static async UniTask GenerateAsset(LicenseGeneratorContext ctx)
        {
            ctx.Info("License asset generation started...");
            if (EditorUtility.DisplayCancelableProgressBar("License Generator", "Reading assets...", 0.0f)) throw new OperationCanceledException();
            var assetPath = AssetDatabase.GetAssetPath(ctx.Settings.MergedLicenseAsset);
            if (ctx.Settings.MergedLicenseAsset == null || string.IsNullOrEmpty(assetPath))
                assetPath = DefaultOutputAssetPath;

            var licenses = new List<LicenseModel>();

            //Read assets
            if (ctx.Settings.IsIncludePackageLicensesEnabled)
            {
                licenses.AddRange(await ListPackageLicensesAsync(ctx));
            }

            if (EditorUtility.DisplayCancelableProgressBar("License Generator", "Listing license assets...", 0.25f)) throw new OperationCanceledException();
            licenses.AddRange(ctx.Settings.IncludedLicenseAssets
                .Select(asset => new LicenseModel
                {
                    LicensedWorkName = asset.name,
                    Text = asset.text
                }));

            if (!string.IsNullOrEmpty(ctx.Settings.IncludedLicensesFolderPath))
                licenses.AddRange(ReadLicenseAssets(ctx.Settings.IncludedLicensesFolderPath));

            //Write merged asset
            if (EditorUtility.DisplayCancelableProgressBar("License Generator", "Writing merged license asset...", 0.5f)) throw new OperationCanceledException();
            AssetDatabase.DeleteAsset(assetPath);
            var mergedContent = string.Join(LicenseSeparator, licenses.Select(l => CreateTextFromLicense(l)));
            await File.WriteAllTextAsync(assetPath, mergedContent);
            AssetDatabase.Refresh();
            ctx.Settings.SetOutputAsset(AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath));

            EditorUtility.DisplayProgressBar("License Generator", "Writing merged license asset...", 0.9f);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.ClearProgressBar();
            ctx.Info("License asset generation finished.");
        }

        /// <summary>
        /// Returns a list of licenses loaded from the project's installed packages.
        /// </summary>
        /// <remarks>
        /// - Ignores packages of built-in Unity modules and features.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1146:Use conditional access.", Justification = "Not supported on Unity Object")]
        private static async UniTask<List<LicenseModel>> ListPackageLicensesAsync(LicenseGeneratorContext ctx)
        {
            var licenses = new List<LicenseModel>();

            var result = await ListInstalledPackages();
            var packages = result
                .Where(r => //filter out built-in modules & features
                    !r.name.StartsWith("com.unity.modules.") &&
                    !r.name.StartsWith("com.unity.feature."))
                .ToList();

            for (var i = 0; i < packages.Count; i++)
            {
                var package = packages[i];
                if (EditorUtility.DisplayCancelableProgressBar("License Generator", $"[{i + 1}/{packages.Count}] - {package.name}", ((float)i) / packages.Count * 0.25f))
                    throw new OperationCanceledException();
                string licenseText;

                if (FindLicenseAtPackageRoot(package.assetPath, out var license))
                {//Load local license file from package if available
                    licenseText = license!.text;
                }
                else if (!string.IsNullOrEmpty(package.licensesUrl) && package.licensesUrl.StartsWith("https://github.com"))
                {//Load license url if available
                    var rawUrl = package.licensesUrl
                        .Replace("https://github.com", "https://raw.githubusercontent.com")
                        .Replace("/blob", string.Empty);
                    licenseText = (await UnityWebRequest.Get(rawUrl).SendWebRequest()).downloadHandler.text;
                }
                else if (ctx.Settings.Assignments.FirstOrDefault(a => a.PackageName == package.name) is var assignment
                    && assignment?.LicenseAsset is var asset && asset != null
                    && asset.text is not null)
                {//Load manually assigned license asset if available
                    licenseText = asset.text;
                }
                else
                {
                    ctx.Error($"Unable to find license for package {package.displayName} ({package.name}). Manual assignment required.");
                    ctx.Settings.AddAssignmentSlot(package.name);
                    continue;
                }

                licenses.Add(new LicenseModel
                {
                    Text = licenseText,
                    LicensedWorkName = package.displayName,
                    HolderName = package.author.name
                });
            }
            return licenses;
        }

        /// <summary>
        /// Returns a list of licenses loaded from all TextAssets in the specified folder, including subdirectories.
        /// </summary>
        /// <param name="folderPath">A folder containing <see cref="TextAsset"/>s.</param>
        private static List<LicenseModel> ReadLicenseAssets(string folderPath)
        {
            var licenses = new List<LicenseModel>();
            if (Directory.Exists(folderPath))
            {
                licenses.AddRange(AssetDatabase
                    .FindAssets("t:TextAsset", new[] { folderPath })
                    .Select(guid => AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(guid)))
                    .Select(asset => new LicenseModel
                    {
                        LicensedWorkName = asset.name,
                        Text = asset.text
                    }));
            }
            return licenses;
        }

        /// <summary>
        /// Helper method to hide ugly PackageManager api
        /// </summary>
        private static UniTask<PackageCollection> ListInstalledPackages()
        {
            var tcs = new UniTaskCompletionSource<PackageCollection>();
            var listPackagesRequest = Client.List();
            EditorApplication.update += onAppUpdate;
            void onAppUpdate()
            {
                if (listPackagesRequest.IsCompleted)
                {
                    EditorApplication.update -= onAppUpdate;
                    if (listPackagesRequest.Status == StatusCode.Success)
                        tcs.TrySetResult(listPackagesRequest.Result);
                    else if (listPackagesRequest.Status >= StatusCode.Failure)
                        tcs.TrySetException(new Exception(listPackagesRequest.Error.message));
                }
            }
            return tcs.Task;
        }

        /// <summary>
        /// Creates a license text similar to the format used in Unity's legal text.
        /// </summary>
        private static string CreateTextFromLicense(LicenseModel license)
        {
            var holderText = !string.IsNullOrEmpty(license.HolderName) ? $"({license.HolderName}) " : string.Empty;
            return $"{license.LicensedWorkName} {holderText}| {license.Text}";
        }

        /// <summary>
        /// Non-recursive search for a license file at given folder.
        /// </summary>
        private static bool FindLicenseAtPackageRoot(string packageAssetPath, out TextAsset? license)
        {
            var licenseAssetPath = AssetDatabase.FindAssets("LICENSE", new[] { packageAssetPath })
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .FirstOrDefault(path => path.Count(c => c is '/') == 2); // only consider assets in package root
            if (licenseAssetPath is null)
            {
                license = null;
                return false;
            }
            else
            {
                license = (TextAsset)AssetDatabase.LoadAssetAtPath(licenseAssetPath, typeof(TextAsset));
                return true;
            }
        }
    }
}
