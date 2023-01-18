#nullable enable
using AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Tests
{
    public class ResourcePathTests
    {
        [Test]
        public void ScenesWork()
        {
            Assert.DoesNotThrow(() => ResourcePaths.Scenes.LoadLoadSceneInitial());
            Assert.DoesNotThrow(() => ResourcePaths.Scenes.LoadLoadSceneNext());
        }

        [Test]
        public void PrefabsWork()
        {
            var prefab = ResourcePaths.Prefabs.LoadCube();
            Assert.NotNull(prefab);
        }

        [Test]
        public void MaterialsWork()
        {
            var cubeMaterial = ResourcePaths.Materials.LoadCube();
            Assert.NotNull(cubeMaterial);

            var cubeAltMaterial = ResourcePaths.Materials.LoadCubeAlt();
            Assert.NotNull(cubeAltMaterial);
        }

        [Test]
        public void AudioClipsWork()
        {
            var coin = ResourcePaths.AudioClips.LoadCoin();
            Assert.NotNull(coin);

            var coinSpin = ResourcePaths.AudioClips.LoadCoinSpin();
            Assert.NotNull(coinSpin);
        }

        [Test]
        public void LayersMasksCorrect()
        {
            var waterLayerFromEnum = ResourcePaths.LayerMasks.Water;
            var waterLayerFromName = LayerMask.GetMask(ResourcePaths.LayerNames.Water);
            Assert.AreEqual(waterLayerFromName, (int)waterLayerFromEnum);
        }

        [Test]
        public void LayersMasksReversable()
        {
            var waterLayerFromEnum = ResourcePaths.LayerMasks.Water;
            var waterLayerFromName = LayerMask.GetMask(ResourcePaths.LayerNames.Water);
            var layerFromEnumName = LayerMask.GetMask(waterLayerFromEnum.ToString());

            Assert.AreEqual(waterLayerFromName, layerFromEnumName);
            Assert.AreEqual((int)waterLayerFromEnum, layerFromEnumName);
        }

        [Test]
        public void AllLayerMaskCorrect()
        {
            var allLayers = ResourcePaths.LayerMasks.All;
            var layers = Enumerable.Range(0, 32)
                .Select(l => LayerMask.LayerToName(l))
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();
            layers.ForEach(l => Assert.IsTrue(allLayers.HasFlag((ResourcePaths.LayerMasks)LayerMask.GetMask(l))));
        }

        [Test]
        public void LayersIndicesCorrect()
        {
            var waterLayerIndexFromEnum = ResourcePaths.LayerIndices.Water;
            var waterLayerIndexFromName = LayerMask.NameToLayer(ResourcePaths.LayerNames.Water);
            Assert.AreEqual((int)waterLayerIndexFromEnum, waterLayerIndexFromName);
        }

        [Test]
        public void LayersIndicesReversable()
        {
            var waterLayerIndexFromEnum = ResourcePaths.LayerIndices.Water;
            var waterLayerIndexFromName = LayerMask.NameToLayer(ResourcePaths.LayerNames.Water);
            var layerIndexFromEnumName = LayerMask.NameToLayer(waterLayerIndexFromEnum.ToString());

            Assert.AreEqual(waterLayerIndexFromName, layerIndexFromEnumName);
            Assert.AreEqual((int)waterLayerIndexFromEnum, layerIndexFromEnumName);
        }
    }
}
