using System;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public class LicenseGenerator
    {
        public void GenerateAssets(LicenseGeneratorContext ctx)
        {
            if(ctx.Settings.IsIncludePackageLicensesEnabled)
            {
                ListPackagesTask().ContinueWith((packages) =>
                {
                    foreach(var package in packages)
                    {
                        ctx.Info(package.licensesUrl);
                    }
                });
            }

            //TODO: implement

        }

        /// <summary>
        /// Helper method to hide ugly PackageManager api
        /// </summary>
        /// <returns></returns>
        private UniTask<PackageCollection> ListPackagesTask()
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
