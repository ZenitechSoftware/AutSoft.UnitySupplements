# Extensibility

The library lets you to inject code into the generation pipeline. To access this feature, create a new Editor project, and reference `AutSoft.UnityResourceGenerator.Editor`. The included functionality is implemented using these mehtods.

## Module generation

A module is single string that is placed inside the main generated class. To create a custom module implement the [IModuleGenerator](xref:AutSoft.UnityResourceGenerator.Editor.Generation.IModuleGenerator) interface. The implementation must provide a parameterless constructor.

## Post processing

A post processor is a piece of code that is run after all modules are produced, and the final file text is created. To create a post processor implement the [IResourcePostProcessor](xref:AutSoft.UnityResourceGenerator.Editor.Generation.IResourcePostProcessor) interface. The implementation must provide a parameterless constructor.

The input of the processor is the current state of the generated file and it returns the new state of the generated file. Post processors also provide a Priority property which determines the ordering of them.

## Using directives

The generated using directives are also customizable under the settings window. If your custom generated code will require other using directives, there is no need to generate them, simply add them in the settings
