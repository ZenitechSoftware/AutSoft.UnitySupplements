# Unity Resource Generator [![openupm](https://img.shields.io/npm/v/com.autsoft.unitysupplements.resourcegenerator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.autsoft.unitysupplements.resourcegenerator/)

Automatically generate a helper class for `Resources.Load` in Unity with the press of a button.

![Generate Button](~/images/ResourceGenerator/intro/GenerateButton.png)

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
 
 If the generate layers option is selected from the following layers:
 
![Layers](~/images/ResourceGenerator/intro/Layers.png)

The following code will also be generated inside the *ResourcePaths.cs* file

```csharp
public static partial class Layers
{

    public const string Default = "Default";
    public static int GetDefaultIndex() => LayerMask.NameToLayer(Default);
    public static int GetDefaultMask() => LayerMask.GetMask(Default);
    public const string TransparentFX = "TransparentFX";
    public static int GetTransparentFXIndex() => LayerMask.NameToLayer(TransparentFX);
    public static int GetTransparentFXMask() => LayerMask.GetMask(TransparentFX);
    public const string IgnoreRaycast = "Ignore Raycast";
    public static int GetIgnoreRaycastIndex() => LayerMask.NameToLayer(IgnoreRaycast);
    public static int GetIgnoreRaycastMask() => LayerMask.GetMask(IgnoreRaycast);
    public const string Water = "Water";
    public static int GetWaterIndex() => LayerMask.NameToLayer(Water);
    public static int GetWaterMask() => LayerMask.GetMask(Water);
    public const string UI = "UI";
    public static int GetUIIndex() => LayerMask.NameToLayer(UI);
    public static int GetUIMask() => LayerMask.GetMask(UI);
    public const string Sample = "Sample";
    public static int GetSampleIndex() => LayerMask.NameToLayer(Sample);
    public static int GetSampleMask() => LayerMask.GetMask(Sample);
}
```

If the generate scene buttons options are selected like this:

![SceneButtonsSettings](~/images/ResourceGenerator/intro/SceneButtonsSettings.png)

These menus will also be created:

![LoadSceneButtons](~/images/ResourceGenerator/intro/LoadSceneButtons.png)

![PlaySceneButtons](~/images/ResourceGenerator/intro/PlaySceneButtons.png)

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unitysupplements.resourcegenerator
```
