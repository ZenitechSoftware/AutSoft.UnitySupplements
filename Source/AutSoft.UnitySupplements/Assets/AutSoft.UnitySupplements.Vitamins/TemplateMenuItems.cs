using UnityEditor;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Buttons that create commonly used classes
    /// </summary>
    public static class TemplateMenuItems
    {
        /// <summary>
        /// Creates a basic AppInstaller.cs file in project root
        /// </summary>
        [MenuItem("Assets/Create/AutSoft/AppInstaller", false,2)]
        private static void CreateAppInstaller() => ProjectWindowUtil.CreateAssetWithContent(
            "AppInstaller.cs",
@"using AutSoft.UnitySupplements.ResourceGenerator.Sample;
using AutSoft.UnitySupplements.Vitamins;
using Injecter.Hosting.Unity;
using Injecter.Unity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Unity3D;
using System;
using System.Reflection;
using UnityEngine;

namespace Namespace
{
    public static class AppInstaller
    {
        public static bool Run { get; set; } = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Install()
        {
            if (!Run) return;

            var logger = new LoggerConfiguration()
                .WriteTo.Unity3D()
                .CreateLogger();

            try
            {
                var host = new HostBuilder()
                    .ConfigureHost(logger)
                    .Build();

                host.Start();
                host.RegisterInjectionsOnSceneLoad();

                Application.quitting += OnQuitting;

                void OnQuitting()
                {
                    host.Dispose();
                    Log.CloseAndFlush();

                    host = null!;

                    Application.quitting -= OnQuitting;
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, ""Host terminated unexpectedly"");
                throw;
            }
        }

        public static IHostBuilder ConfigureHost(this IHostBuilder builder, Serilog.ILogger logger)
        {
            var jsons = new[]
            {
                ResourcePaths.TextAssets.LoadAppSettings()
            };

            return builder
                .UseUnity(_ => { }, false, false, jsons)
                .ConfigureServices(ConfigureServices)
                .UseDefaultServiceProvider(o =>
                {
                    o.ValidateOnBuild = true;
                    o.ValidateScopes = true;
                })
                .UseSerilog(logger);
        }

        public static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
        {
            var assemblies = new[] { typeof(AppInstaller).Assembly };

            services.AddSceneInjector(
                injecterOptions => injecterOptions.UseCaching = true,
                sceneInjectorOptions =>
                {
                    sceneInjectorOptions.DontDestroyOnLoad = true;
                    sceneInjectorOptions.InjectionBehavior = SceneInjectorOptions.Behavior.Factory;
                });

            services.AddHostedServices(assemblies);

            services.AddSingleton<ICancellation, Cancellation>();
        }

        private static void AddHostedServices(this IServiceCollection services, params Assembly[] assemblies) =>
            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo<IHostedService>())
                .AsSelfWithInterfaces()
                .WithSingletonLifetime());
    }
}

");
    }
}
