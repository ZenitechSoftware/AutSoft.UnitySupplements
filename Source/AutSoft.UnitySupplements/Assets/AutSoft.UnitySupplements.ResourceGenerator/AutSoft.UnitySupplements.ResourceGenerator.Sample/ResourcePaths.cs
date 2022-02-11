using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutSoft.UnitySupplements.ResourceGenerator.Sample
{
    // ReSharper disable PartialTypeWithSinglePart
    // ReSharper disable InconsistentNaming
    // ReSharper disable IncorrectBlankLinesNearBraces
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable RCS1036 // Remove redundant empty line.
    public static partial class ResourcePaths
    {
        public static partial class Scenes
        {

            public const string CreatePrefab = "AutSoft.UnitySupplements.ResourceGenerator.Sample/Scenes/CreatePrefab";
            public static void LoadCreatePrefab(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadScene(CreatePrefab, mode);
            public static AsyncOperation LoadAsyncCreatePrefab(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadSceneAsync(CreatePrefab, mode);

            public const string LoadSceneInitial = "AutSoft.UnitySupplements.ResourceGenerator.Sample/Scenes/LoadSceneInitial";
            public static void LoadLoadSceneInitial(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadScene(LoadSceneInitial, mode);
            public static AsyncOperation LoadAsyncLoadSceneInitial(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadSceneAsync(LoadSceneInitial, mode);

            public const string LoadSceneNext = "AutSoft.UnitySupplements.ResourceGenerator.Sample/Scenes/LoadSceneNext";
            public static void LoadLoadSceneNext(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadScene(LoadSceneNext, mode);
            public static AsyncOperation LoadAsyncLoadSceneNext(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadSceneAsync(LoadSceneNext, mode);

        }

        public static partial class Prefabs
        {

            public const string Cube = "Cube";
            public static GameObject LoadCube() => Resources.Load<GameObject>(Cube);

        }

        public static partial class Materials
        {

            public const string Cube = "Cube";
            public static Material LoadCube() => Resources.Load<Material>(Cube);

            public const string CubeAlt = "CubeAlt";
            public static Material LoadCubeAlt() => Resources.Load<Material>(CubeAlt);

            public const string LiberationSansSDF_DropShadow = "Fonts & Materials/LiberationSans SDF - Drop Shadow";
            public static Material LoadLiberationSansSDF_DropShadow() => Resources.Load<Material>(LiberationSansSDF_DropShadow);

            public const string LiberationSansSDF_Outline = "Fonts & Materials/LiberationSans SDF - Outline";
            public static Material LoadLiberationSansSDF_Outline() => Resources.Load<Material>(LiberationSansSDF_Outline);

        }

        public static partial class AudioClips
        {

            public const string CoinSpin = "Coin Spin";
            public static AudioClip LoadCoinSpin() => Resources.Load<AudioClip>(CoinSpin);

            public const string _1Coin1 = "1Coin 1";
            public static AudioClip Load_1Coin1() => Resources.Load<AudioClip>(_1Coin1);

            public const string Coin = "Coin";
            public static AudioClip LoadCoin() => Resources.Load<AudioClip>(Coin);

        }

        public static partial class TextAssets
        {

            public const string LineBreakingFollowingCharacters = "LineBreaking Following Characters";
            public static TextAsset LoadLineBreakingFollowingCharacters() => Resources.Load<TextAsset>(LineBreakingFollowingCharacters);

            public const string LineBreakingLeadingCharacters = "LineBreaking Leading Characters";
            public static TextAsset LoadLineBreakingLeadingCharacters() => Resources.Load<TextAsset>(LineBreakingLeadingCharacters);

        }

    }
}