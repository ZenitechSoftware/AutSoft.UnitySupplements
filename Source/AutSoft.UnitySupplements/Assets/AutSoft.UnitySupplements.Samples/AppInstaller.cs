#nullable enable
using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.ResourceGenerator.Sample;
using AutSoft.UnitySupplements.Timeline;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using Injecter.Unity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Unity3D;
using System;
using System.IO;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples
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
                var serviceProvider = new ServiceCollection()
                    .Configure(logger)
                    .BuildServiceProvider(true);

                CompositionRoot.ServiceProvider = serviceProvider;

                Application.quitting += OnQuitting;

                void OnQuitting()
                {
                    Log.CloseAndFlush();

                    serviceProvider = null!;

                    Application.quitting -= OnQuitting;
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Host terminated unexpectedly");
                throw;
            }
        }

        public static IServiceCollection Configure(this IServiceCollection serviceCollection, Serilog.ILogger logger)
        {
            var jsons = new[]
            {
                ResourcePaths.TextAssets.LoadAppSettings(),
            };

            var config = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(jsons[0].bytes))
                .Build();

            return serviceCollection
                .AddLogging(b => b.AddSerilog(logger))
                .ConfigureServices(config);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1163:Unused parameter.", Justification = "Will be used later")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Will be used later")]
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfigurationRoot config)
        {
            var assemblies = new[] { typeof(AppInstaller).Assembly };

            services.AddSceneInjector(
                injecterOptions => injecterOptions.UseCaching = true,
                sceneInjectorOptions =>
                {
                    sceneInjectorOptions.DontDestroyOnLoad = true;
                    sceneInjectorOptions.InjectionBehavior = SceneInjectorOptions.Behavior.CompositionRoot;
                });

            services.AddEventBus(assemblies);

            services.AddSingleton<ICancellation, Cancellation>();

            services.AddTimeline();

            return services;
        }
    }
}
