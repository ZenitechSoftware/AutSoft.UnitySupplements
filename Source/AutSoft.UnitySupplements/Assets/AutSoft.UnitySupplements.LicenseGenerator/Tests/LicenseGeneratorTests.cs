#nullable enable
using AutSoft.UnitySupplements.LicenseGenerator.Editor;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace AutSoft.UnitySupplements.LicenseGenerator.Tests
{
    public class LicenseGeneratorTests
    {
        private const string TestAssetsPath = "Assets/AutSoft.UnitySupplements.LicenseGenerator/Tests/";
        private string OutputAssetPath => $"{TestAssetsPath}/Output.txt";

        [SetUp, OneTimeTearDown]
        public void ClearOutputAsset()
        {
            AssetDatabase.DeleteAsset(OutputAssetPath);
            File.WriteAllText(OutputAssetPath, string.Empty);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [UnityTest]
        public IEnumerator IncludeAssetSingle() => UniTask.ToCoroutine(async () =>
        {
            var settings = AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>($"{TestAssetsPath}/SingleAssetCase.asset");

            await Editor.LicenseGenerator.GenerateAsset(new(settings));
            AssertOutputNotEmpty();
        });

        [UnityTest]
        public IEnumerator IncludePackageLicenses() => UniTask.ToCoroutine(async () =>
        {
            var settings = AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>($"{TestAssetsPath}/PackagesCase.asset");

            await Editor.LicenseGenerator.GenerateAsset(new(settings));
            AssertOutputNotEmpty();
        });

        [UnityTest]
        public IEnumerator IncludeAssetsFolder() => UniTask.ToCoroutine(async () =>
        {
            var settings = AssetDatabase.LoadAssetAtPath<LicenseGeneratorSettings>($"{TestAssetsPath}/IncludeFolderCase.asset");

            await Editor.LicenseGenerator.GenerateAsset(new(settings));
            AssertOutputNotEmpty();
        });

        private void AssertOutputNotEmpty() => Assert.IsNotEmpty(
            AssetDatabase.LoadAssetAtPath<TextAsset>(OutputAssetPath).text);
    }
}
