using Nuke.Common.Tooling;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.Unity;
using Nuke.Common.Utilities.Collections;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Nuke.Common.Tools.Chocolatey.ChocolateyTasks;

static class UnityHelper
{
    public static string UnityVersion(string unityProjectPath) =>
        Regex.Match
            (
                File.ReadAllLines(Path.Combine(unityProjectPath, "ProjectSettings", "ProjectVersion.txt"))[0],
                @"(\d+.\d+.\d+.*)"
            )
            .Value;

    public static async Task ChocoInstallUnity(string unityProjectPath, string? unityModule = null)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Log.Information("Skipping Unity install on non-windows");
            return;
        }

        var unityVersion = UnityVersion(unityProjectPath);
        var unityVersionSmall = unityVersion.Replace("f1", string.Empty);

        Chocolatey($@"install unity --params=""'/InstallationPath:C:\Program Files\Unity\Hub\Editor\{unityVersion}'"" --version={unityVersionSmall} --no-progress -y --ignore-package-exit-codes=3010");

        if (unityModule is null) return;

        Chocolatey($@"install unity-{unityModule} --params=""'/InstallationPath:C:\Program Files\Unity\Hub\Editor\{unityVersion}'"" --version={unityVersionSmall} --no-progress -y --ignore-package-exit-codes=3010");

        await StopUnity();
    }

    public static UnitySettings ConfigureCustom
    (
        this UnitySettings s,
        string projectPath,
        string? unityPassword,
        string? unityEmail,
        string? unitySerial,
        bool isCi,
        string? buildMethod = null
    )
    {
        s = SetCredentials(s, unityPassword, unityEmail, unitySerial, isCi);

        s = SetUnityInstallPath(s, projectPath);

        s = s
            .SetProjectPath(projectPath)
            .EnableBatchMode()
            .EnableNoGraphics()
            .EnableQuit()
            .AddCustomArguments
            (
                "-buildVersion"
            );

        if (buildMethod is not null) s = s.SetExecuteMethod(buildMethod);

        return s;
    }

    private static UnitySettings SetCredentials
    (
        UnitySettings s,
        string? unityPassword,
        string? unityEmail,
        string? unitySerial,
        bool isCi
    )
    {
        if (isCi)
        {
            if (string.IsNullOrWhiteSpace(unityPassword)
                || string.IsNullOrWhiteSpace(unityEmail)
                || string.IsNullOrWhiteSpace(unitySerial))
            {
                throw new InvalidOperationException("Unity credentials are not set");
            }

            s = s
                .SetPassword(unityPassword)
                .SetUsername(unityEmail)
                .SetSerial(unitySerial);
        }

        return s;
    }

    private static UnitySettings SetUnityInstallPath(UnitySettings s, string projectPath) =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? s.SetHubVersion(UnityVersion(projectPath))
            : s.SetProcessToolPath("/opt/unity/Editor/Unity");

    public static async Task StopUnity()
    {
        var processes = Process.GetProcessesByName("Unity");

        if (processes.Length == 0)
        {
            Log.Information("No Unity processes found");
        }

        processes.ForEach(p =>
        {
            Log.Information("Killing process {0}", p.ProcessName);
            p.Kill();
        });

        await Task.Delay(TimeSpan.FromSeconds(5));
    }
}
