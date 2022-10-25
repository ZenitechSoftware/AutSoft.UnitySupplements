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
        public async UniTask GenerateAssets(LicenseGeneratorContext ctx)
        {
            var packageModels = new List<PackageModel>();

            if(ctx.Settings.IsIncludePackageLicensesEnabled)
            {
                packageModels.AddRange(await ListPackageLicensesAsync(ctx));
            }

            ctx.Settings.IncludedLicenseAssets.Select(licenseAsset => licenseAsset.text);

            if(!string.IsNullOrEmpty(ctx.Settings.IncludedLicensesFolderPath))
            {
                packageModels.AddRange(await ReadLicenseAssetsAsync(ctx.Settings.IncludedLicensesFolderPath));
            }

            //TODO: implement



        }

        private async UniTask<List<PackageModel>> ListPackageLicensesAsync(LicenseGeneratorContext ctx)
        {
            var packageLicenses = new List<PackageModel>();

            var result = await ListInstalledPackages();
            var packages = result
                .Where(r => !r.name.StartsWith("com.unity.modules."));//filter out built-in modules

            foreach (var package in packages)
            {
                var item = new PackageModel { Name = package.displayName };

                var guids = AssetDatabase.FindAssets("LICENSE", new[] { package.assetPath });
                if (guids.Length > 0)
                {//Load local license file if available
                    var licensePaths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g)).ToArray();
                    item.LicenseText = ((TextAsset)AssetDatabase.LoadAssetAtPath(licensePaths.Single(), typeof(TextAsset))).text;
                }
                else if (package.licensesUrl is not null)
                {//Load license url if available
                    if (package.licensesUrl.StartsWith("https://github.com"))
                    {
                        var rawUrl = package.licensesUrl
                            .Replace("https://github.com", "https://raw.githubusercontent.com")
                            .Replace("/blob", string.Empty);
                        item.LicenseText = (await UnityWebRequest.Get(rawUrl).SendWebRequest()).downloadHandler.text;
                    }
                    else
                    {
                        ctx.Error($"Unable to download license for package {package.displayName} from {package.licensesUrl}");
                        continue;
                    }
                }
                else
                {
                    ctx.Error($"Unable to find license for package {package.displayName}");
                    continue;
                }
                packageLicenses.Add(item);
            }
            return packageLicenses;
        }

        private async UniTask<List<PackageModel>> ReadLicenseAssetsAsync(string folderPath)
        {
            var packageLicenses = new List<PackageModel>();
            if(Directory.Exists(folderPath))
            {
                packageLicenses.AddRange(AssetDatabase
                    .LoadAllAssetsAtPath(folderPath)
                    .Select(o => new PackageModel
                    {
                        Name = o.name,
                        LicenseText = ((TextAsset)o).text
                    }));
            }
            return packageLicenses;
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

    }
}
