using AutSoft.UnitySupplements.ResourceGenerator.Sample;
using NUnit.Framework;

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
    }
}
