# Project sample
This article demonstrates the process of how to setup Unity projects to be able to use the supplement packages.

## Adding eventbus
After creating the project we can add one of our packages to the project with openupm. For example to add the eventbus package open a terminel in the new project root and type the following command:
```
openupm add com.autsoft.unitysupplements.eventbus
```
After adding this you will see multiple errors appear in your console, this is because some dependecies from nuget still need to be added.
## Adding nuget scope and packages
To be able to add nuget packages first we need to add the following scope to the **scopedRegistries** array inside the *Packages/manifest.json* file.
```{
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": [
        "org.nuget"
      ]
}
```

After that you can add nuget packages in the package manager window according to the Depenedcies described [here](xref:Dependencies.md).

![PackageManager](~/images/ProjectSample/PackageManager.png)

Adding these dependencies fixes all the errors we had.

## Adding Injecter DI
To make it possible to inject the eventbus into monobeheviours we are going to use [Injecter](https://openupm.com/packages/com.injecter.unity/) and [Injecter.Hosting](https://openupm.com/packages/com.injecter.hosting.unity/?subPage=deps) from OpenUPM with the following command.
```
openupm add com.injecter.hosting.unity
```
Once again we need to add:
- [Injecter](https://www.nuget.org/packages/Injecter/)  
- [Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)
- [Serilog.Hosting](https://www.nuget.org/packages/Serilog.Extensions.Hosting/)
  
Nuget packages to get rid of the errors.