using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.MSBuild;
using System;
using System.IO;
using System.Threading.Tasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.DocFX.DocFXTasks;
using static Nuke.Common.Tools.Unity.UnityTasks;
using static UnityHelper;
// ReSharper disable InconsistentNaming

[CheckBuildProjectConfigurations]
class Build : NukeBuild
{
    const string DocFxJsonPath = "Documentation/docfx.json";

    public static int Main() => Execute<Build>(x => x.CompileUnity);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Password for Unity license")] string? UnityPassword;
    [Parameter("Email for Unity license")] string? UnityEmail;
    [Parameter("Serial for Unity license")] string? UnitySerial;

    [Parameter("Are we running in CI")] bool IsCi = false;

    static AbsolutePath UnityProjectPath => RootDirectory / "Source" / "AutSoft.UnitySupplements";
    static AbsolutePath UnitySolution => UnityProjectPath / "AutSoft.UnitySupplements.sln";

  
    Target GenerateUnitySolution => _ => _
        .DependsOn(InstallUnity)
        .OnlyWhenDynamic(() => IsCi)
        .Triggers(ReturnLicense)
        .Executes(async () =>
        {
            async Task GenerateSolution()
            {
                await StopUnity();

                Unity(s => s
                    .ConfigureCustom
                    (
                        UnityProjectPath,
                        UnityPassword,
                        UnityEmail,
                        UnitySerial,
                        IsCi,
                        "AutSoft.UnitySupplements.Samples.BuildHelper.RegenerateProjectFiles"
                    ));
            }

            await GenerateSolution();

            // Changing editor preferences are only applied after restart
            if (IsCi) await GenerateSolution();
        });

    Target CompileUnity => _ => _
        .DependsOn(GenerateUnitySolution)
        .Executes(() =>
        {
            static string? GetMsBuildPath()
            {
                const string vs2019 = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe";
                const string vs2022 = @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe";
                if (File.Exists(vs2019))
                {
                    return vs2019;
                }
                else if (File.Exists(vs2022))
                {
                    return vs2022;
                }
                return null;
            }

            MSBuild(s =>
            {
                s = s
                    .SetTargetPath(UnitySolution)
                    .SetTargets("Rebuild")
                    .SetConfiguration(Configuration.Debug)
                    .SetMaxCpuCount(Environment.ProcessorCount)
                    .SetNodeReuse(IsLocalBuild);

                var msBuildPath = GetMsBuildPath();
                if (msBuildPath is not null) s = s.SetProcessToolPath(msBuildPath);

                return s;
            });
        });

    Target ReturnLicense => _ => _
        .OnlyWhenDynamic(() => IsCi)
        .AssuredAfterFailure()
        .Executes(async () =>
        {
            await StopUnity();

            Unity(s => s
                .ConfigureCustom
                (
                    UnityProjectPath,
                    UnityPassword,
                    UnityEmail,
                    UnitySerial,
                    IsCi
                )
                .SetProcessArgumentConfigurator(a => a.Add("-returnlicense")));
        });

    Target InstallUnity => _ => _
        .OnlyWhenDynamic(() => IsCi)
        .Executes(async () => await ChocoInstallUnity(UnityProjectPath));


    Target CreateMetadata => _ => _
        .DependsOn(CompileUnity)
        .Executes(() => DocFX($"metadata {DocFxJsonPath}"));

    Target BuildDocs => _ => _
        .DependsOn(CreateMetadata)
        .Executes(() => DocFX($"build {DocFxJsonPath}"));

    Target ServeDocs => _ => _
        .DependsOn(BuildDocs)
        .Executes(() => DocFX($"{DocFxJsonPath} --serve"));

}
