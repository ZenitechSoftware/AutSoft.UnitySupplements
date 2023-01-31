#nullable enable
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AutSoft.UnitySupplements.EventBus
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to add EventBus functionality.
    /// </summary>
    public static class DiHelper
    {
        /// <summary>
        /// Adds <see cref="IEventBus"/> implemented by <see cref="SimpleEventBus"/> as a singleton service.
        /// </summary>
        /// <param name="assemblies">Assemblies where <see cref="IEventHandler{T}"/> is implemented.</param>
        public static void AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventBus, SimpleEventBus>();
            services.AddTransient<ServiceFactory>(sp => sp.GetRequiredService);

            var eventHandlerType = typeof(IEventHandler<>);
            foreach (var type in assemblies
                         .SelectMany(a => a.GetTypes())
                         .Where(t => t.GetInterfaces()
                                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == eventHandlerType)))
            {
                if (type.IsAbstract || type.IsInterface) continue;
                if (!type.IsGenericType)
                {
                    var eventType = type.GetInterfaces().First(i => i.GetGenericTypeDefinition() == eventHandlerType).GetGenericArguments()[0];
                    services.AddTransient(eventHandlerType.MakeGenericType(eventType), type);
                }
                else
                {
                    services.AddTransient(eventHandlerType, type);
                }
            }
        }
    }
}
