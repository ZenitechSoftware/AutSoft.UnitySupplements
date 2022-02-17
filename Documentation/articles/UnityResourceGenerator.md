# Unity Resource Generator

Automatically generate a helper class for `Resources.Load` in Unity with the press of a button.

![Generate Button](~/images/intro/GenerateButton.png)

With this folder structure:

```
Assets/
├─ Resources/
│  ├─ Coin.prefab
│  ├─ Coin.mp3
├─ Scenes/
│  ├─ CoinRain.unity
```

The following helper class is generated:

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sample
{
    // ReSharper disable PartialTypeWithSinglePart
    public static partial class ResourcePaths
    {

        public static partial class Scenes
        {
            public const string CoinRain = "Scenes/CoinRain";

            public static void LoadCoinRain(LoadSceneMode mode = LoadSceneMode.Single) =>
                SceneManager.LoadScene(CoinRain, mode);

            public static AsyncOperation LoadAsyncCoinRain(LoadSceneMode mode = LoadSceneMode.Single) =>
                SceneManager.LoadSceneAsync(CoinRain, mode);
        }

        public static partial class Prefabs
        {
            public const string Coin = "Coin";
            public static GameObject LoadCube() => Resources.Load<GameObject>(Coin);
        }

        public static partial class AudioClips
        {
            public const string Coin = "Coin";
            public static AudioClip LoadCoin() => Resources.Load<AudioClip>(Coin);
        }
    }
}
```

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unityresourcegenerator
```
