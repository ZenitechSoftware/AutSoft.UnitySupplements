# Getting Started

The Unity Resource Generator is a Unity Editor tool which can generate a helper class which will create a helper class that contains paths to loadable Unity resources. Additionally it will also generate helper methods to load scenes.

The generation of the class is customizable by default and the library also lets developers create custom code to run during generation.

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unityresourcegenerator
```

## Running the tool

The tool will create new button in the Editor at `Tools / Generate Resource Paths`

![Generate Button](~/images/intro/GenerateButton.png)

If your click the button the helper class will be generated in the root of the `Assets` folder

## Name generation rules

The generated method and filed names are slightly different from the actual file names, because not all valid file names are valid C# identifier names. The following rules apply:

- Whitespaces are removed
- If the file name starts with a number a `_` character is added to the start of the identifier
- All non-alphanumeric characters are replaced by the `_` character
