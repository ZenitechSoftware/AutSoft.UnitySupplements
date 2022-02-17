# Customization

After pressing the `Generate Resource Paths` button, or visiting the `ResourceGenerator` page in the Project Settings an asset will be generated in the Assets folder, which will contain settings for the code generation.

## Built-in settings

![Generate Button](~/images/Customization/DefaultSettings.png)

### Basic settings

- **Folder from Assets**: Relative path to the folder where you want the generated class
- **Namespace**: The namespace of the generated class
- **Class name**: The name of the generated class. The file name will be the same
- **Log Infos**: Whether to log Info level logs. Turned off by default
- **Log Errors**: Whether to log Error level logs. Turned on by default

### Advanced settings

> [!NOTE]
> If you unrecoverably mess up your settings press the `Reset file mappings` button to restore the default file mappings

The data section describes the automatically generated file mappings. The data is an array. For each element of the array it will create a new inner class of the main generated one.

- **Class Name**: The name of the generated class
- **File Extensions**: Search pattern for the files
- **Data Type**: The C# type returned by `Resurces.Load`

The default mappings are created from the [Unity documentation](https://docs.unity3d.com/Manual/BuiltInImporters.html). If you know of any valid file mapping that the documentation does not state, please fork and create a Pull Request, or create an [Issue](https://github.com/AutSoft/UnityResourceGenerator/issues/new).

> [!WARNING]
> Scenes have special handling during the file generation, because of the way scenes paths are used by the SceneManager. Scenes are identified by the file ending `.unity`

### Other settings

- **Generate layers**: Wheter to generate layers functions
- **Generate scene buttons**: Wheter to generate load scene buttons to Unity menu
  - **Scene names**: Names of the scenes for which play buttons will also    be created

## Extending the generated classes

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
