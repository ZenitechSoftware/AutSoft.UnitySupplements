# Unity Resource Generator [![openupm](https://img.shields.io/npm/v/com.autsoft.unitysupplements.resourcegenerator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.autsoft.unitysupplements.resourcegenerator/)

## Getting Started

The Unity Resource Generator is a Unity Editor tool which can generate a helper class which will create a helper class that contains paths to loadable Unity resources. Additionally it will also generate helper methods to load scenes.

The generation of the class is customizable by default and the library also lets developers create custom code to run during generation.

### Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unitysupplements.resourcegenerator
```

### Running the tool

The tool will create new button in the Editor at `Tools / Generate Resource Paths`

![Generate Button](~/images/ResourceGenerator/intro/GenerateButton.png)

When you click the button the helper class will be generated in the root of the `Assets` folder.

### Name generation rules

The generated method and filed names are slightly different from the actual file names, because not all valid file names are valid C# identifier names. The following rules apply:

- Whitespaces are removed
- If the file name starts with a number a `_` character is added to the start of the identifier
- All non-alphanumeric characters are replaced by the `_` character

### Generator

Automatically generate a helper class for `Resources.Load` in Unity with the press of a button.

With this folder structure:


### Resources

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

### Layers

If the generate layers option is selected from the following layers:
 
![Layers](~/images/ResourceGenerator/intro/Layers.png)

The following code will also be generated inside the *ResourcePaths.cs* file

```csharp
[Flags]
public enum LayerMasks
{
    None = 0,
    Default = 1 << 0,
    TransparentFX = 1 << 1,
    IgnoreRaycast = 1 << 2,
    Water = 1 << 4,
    UI = 1 << 5,
    SpatialAwareness = 1 << 31,
    All = Default | TransparentFX | IgnoreRaycast | Water | UI | SpatialAwareness
}

public enum LayerIndices
{
    Default = 0,
    TransparentFX = 1,
    IgnoreRaycast = 2,
    Water = 4,
    UI = 5,
    SpatialAwareness = 31,
}

public static partial class LayerNames
{
    public const string Default = "Default";
    public const string TransparentFX = "TransparentFX";
    public const string IgnoreRaycast = "Ignore Raycast";
    public const string Water = "Water";
    public const string UI = "UI";
    public const string SpatialAwareness = "Spatial Awareness";
}
```

### Scene buttons

If the generate scene buttons options are selected like this:

![SceneButtonsSettings](~/images/ResourceGenerator/intro/SceneButtonsSettings.png)

These menus will also be created:

![LoadSceneButtons](~/images/ResourceGenerator/intro/LoadSceneButtons.png)

![PlaySceneButtons](~/images/ResourceGenerator/intro/PlaySceneButtons.png)

### Editor preferences

Generates strongly typed, categorized, static methods to set Editor preferences. Known and useful preferences will be added in later releases. You can use the reset button to generate the current up-to-date version. The keys can be explored through the [platform specific storages explained in the documentation](https://docs.unity3d.com/ScriptReference/EditorPrefs.html).

![EditorPreferences](~images/ResourceGenerator/EditorPreferences.png)

For browsing the current editor preferences it is advised to use the [PlayerPrefsEditor](https://github.com/sabresaurus/PlayerPrefsEditor) project

## Customization

After pressing the `Generate Resource Paths` button, or visiting the `AutSoft/Resource Generator` page in the Project Settings an asset will be generated in the Assets folder, which will contain settings for the code generation.

### Built-in settings

![Generate Button](~/images/ResourceGenerator/Customization/DefaultSettings.png)

#### Basic settings

- **Folder from Assets**: Relative path to the folder where you want the generated class
- **Namespace**: The namespace of the generated class
- **Class name**: The name of the generated class. The file name will be the same
- **Log Infos**: Whether to log Info level logs. Turned off by default
- **Log Errors**: Whether to log Error level logs. Turned on by default

#### Advanced settings

> [!NOTE]
> If you unrecoverably mess up your settings press the `Reset file mappings` button to restore the default file mappings

The data section describes the automatically generated file mappings. The data is an array. For each element of the array it will create a new inner class of the main generated one.

- **Class Name**: The name of the generated class
- **File Extensions**: Search pattern for the files
- **Data Type**: The C# type returned by `Resurces.Load`

The default mappings are created from the [Unity documentation](https://docs.unity3d.com/Manual/BuiltInImporters.html). If you know of any valid file mapping that the documentation does not state, please fork and create a Pull Request, or create an [Issue](https://github.com/AutSoft/UnityResourceGenerator/issues/new).

> [!WARNING]
> Scenes have special handling during the file generation, because of the way scenes paths are used by the SceneManager. Scenes are identified by the file ending `.unity`

#### Other settings

- **Generate layers**: Whether to generate layers functions
- **Generate scene buttons**: Whether to generate load scene buttons to Unity menu
  - **Scene names**: Names of the scenes for which play buttons will also    be created

### Extending the generated classes

The generated classes are `partial` by default. This means that if you want to add custom methods, simply create another `partial` class and add your code there.

```csharp
public static partial class ResourcePaths
{
    public static partial class Scenes
    {
        public static string[] MainScenes { get; } = new []
        {
            Coins,
            Level1,
            Level2,
            // ...
        }
    }
}
```

## Extensibility

The library lets you to inject code into the generation pipeline. To access this feature, create a new Editor project, and reference `AutSoft.UnityResourceGenerator.Editor`. The included functionality is implemented using these methods.

### Module generation

A module is single string that is placed inside the main generated class. To create a custom module implement the [IModuleGenerator](xref:AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.IModuleGenerator) interface. The implementation must provide a parameterless constructor.

### Post processing

A post processor is a piece of code that is run after all modules are produced, and the final file text is created. To create a post processor implement the [IResourcePostProcessor](xref:AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.IResourcePostProcessor) interface. The implementation must provide a parameterless constructor.

The input of the processor is the current state of the generated file and it returns the new state of the generated file. Post processors also provide a Priority property which determines the ordering of them.

### Using directives

The generated using directives are also customizable under the settings window. If your custom generated code will require other using directives, there is no need to generate them, simply add them in the settings

## Known issues

### 0.1.0 - *

- Duplicate file names in the same class module will break the generated file. The solution for this requires further investigation
