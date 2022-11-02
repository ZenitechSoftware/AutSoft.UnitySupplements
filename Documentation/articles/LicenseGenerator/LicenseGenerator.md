# License Generator [![openupm](https://img.shields.io/npm/v/com.autsoft.unitysupplements.licensegenerator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.autsoft.unitysupplements.licensegenerator/)

## Introduction
The License Generator is a Unity Editor tool that helps managing licenses of packages and assets used throughout a Unity project by generating a merged license `TextAsset`. See below for installation, usage & config.

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unitysupplements.licensegenerator
```

## Running the tool

The tool will create new button in the Editor at `Tools / Generate License Asset`

![Tool Button](~/images/LicenseGenerator/ToolButton.jpg)

Clicking the button generates a `TextAsset` as configured in the settings.

## Configuration

A `LicenseGeneratorSettings` asset with default configuration is created upon first use. This may be moved to any preferred asset folder.
To configure, modify this asset or open the `Project Settings` window and see the `AutSoft/License Generator` page.

![Project Settings page](~/images/LicenseGenerator/ProjectSettingsPage.jpg)

### Settings:
- **Include package licenses**: Whether to include licenses from packages used in the project.
- **Manual assignments**: This is a list of packages with missing license information. Assign a `TextAsset` to each slot to fix generator warnings.
- **Additional licenses folder**: Optionally provide a folder path here that contains license assets. If this option is not empty, all TextAssets from the path will be loaded, including subdirectories. Example: `Assets/My3DModelLicenses`.
- **Additional license text assets**: Optionally provide licenses here to include in the generated asset. Use the <kbd>+</kbd> icon to add a new item.
- **Merged license asset**: Select a `TextAsset` to write the generated content into. Existing content is overwritten. If nothing is provided, a new asset is created to `Assets/Licenses.txt`.

## Remarks
- Console messages warn of any missing package license information, which will disappear once manually assigned.
- If a package only contains the url to the license which points to a Github repo, a reqest is made to query the license file.
- Package licenses for Unity built-in modules and features are ignored.
- The tool works well alongside [ResourceGenerator](articles/ResourceGenerator/UnityResourceGenerator.md). See the `LegalsSampleScene` in the Samples project.