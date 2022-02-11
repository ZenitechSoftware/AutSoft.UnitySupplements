#nullable enable
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AutSoft.UnitySupplements.EventBus
{
    public static class DiHelper
    {
        public static void AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventBus, SimpleEventBus>();
            services.AddTransient<ServiceFactory>(sp => sp.GetRequiredService);

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes =>
                    classes.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
