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
    public class LicenseGenerator
    {
        private string LicenseSeparator =>
            Environment.NewLine + $"+ + + + + + + + + + + + + + + +" +
            Environment.NewLine + Environment.NewLine;

        public async UniTask GenerateAsset(LicenseGeneratorContext ctx)
        {
            var assetPath = AssetDatabase.GetAssetPath(ctx.Settings.MergedLicenseAsset);
            if (ctx.Settings.MergedLicenseAsset is null || string.IsNullOrEmpty(assetPath))
            {
                ctx.Error("Merged license asset is unset.");
                return;
            }

            var licenses = new List<LicenseModel>();

            //Read assets

            if(ctx.Settings.IsIncludePackageLicensesEnabled)
                licenses.AddRange(await ListPackageLicensesAsync(ctx));

            ctx.Settings.IncludedLicenseAssets.Select(licenseAsset => licenseAsset.text);

            if(!string.IsNullOrEmpty(ctx.Settings.IncludedLicensesFolderPath))
                licenses.AddRange(await ReadLicenseAssetsAsync(ctx.Settings.IncludedLicensesFolderPath));

            //Write merged asset
            AssetDatabase.DeleteAsset(assetPath);
            var mergedContent = string.Join(LicenseSeparator, licenses.Select(l => CreateTextFromLicense(l)));
            await File.WriteAllTextAsync(assetPath, mergedContent);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ctx.Info("License asset generated.");
        }

        private async UniTask<List<LicenseModel>> ListPackageLicensesAsync(LicenseGeneratorContext ctx)
        {
            var licenses = new List<LicenseModel>();

            var result = await ListInstalledPackages();
            var packages = result
                .Where(r => !r.name.StartsWith("com.unity.modules.")) //filter out built-in modules
                .Where(r => !r.name.StartsWith("com.unity.feature.")); //filter out built-in features

            foreach (var package in packages)
            {
                string licenseText;

                if (AssetDatabase.FindAssets("LICENSE", new[] { package.assetPath }) is var guids && guids.Length > 0)
                {//Load local license file from package if available
                    var licensePaths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g)).ToArray();
                    licenseText = ((TextAsset)AssetDatabase.LoadAssetAtPath(licensePaths.Single(), typeof(TextAsset))).text;
                }
                else if (!string.IsNullOrEmpty(package.licensesUrl) && package.licensesUrl.StartsWith("https://github.com"))
                {//Load license url if available
                    var rawUrl = package.licensesUrl
                        .Replace("https://github.com", "https://raw.githubusercontent.com")
                        .Replace("/blob", string.Empty);
                    licenseText = (await UnityWebRequest.Get(rawUrl).SendWebRequest()).downloadHandler.text;
                }
                else if(ctx.Settings.Assignments.FirstOrDefault(a => a.PackageName == package.name) is var assignment
                    && assignment?.LicenseAsset is var asset && asset != null
                    && asset.text is not null)
                {//Load manually assigned license asset if available
                    licenseText = assignment.LicenseAsset.text;
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

        private async UniTask<List<LicenseModel>> ReadLicenseAssetsAsync(string folderPath)
        {
            var licenses = new List<LicenseModel>();
            if(Directory.Exists(folderPath))
            {
                licenses.AddRange(AssetDatabase
                    .LoadAllAssetsAtPath(folderPath)
                    .Select(o => new LicenseModel
                    {
                        LicensedWorkName = o.name,
                        Text = ((TextAsset)o).text
                    }));
            }
            return licenses;
        }

        /// <summary>
        /// Helper method to hide ugly PackageManager api
        /// </summary>
        private UniTask<PackageCollection> ListInstalledPackages()
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
        private string CreateTextFromLicense(LicenseModel license)
        {
            var holderText = !string.IsNullOrEmpty(license.HolderName) ? $"({license.HolderName}) " : string.Empty;
            return $"{license.LicensedWorkName} {holderText}| {license.Text}";
        }
    }
}
