#nullable enable
using Microsoft.Extensions.DependencyInjection;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    public static class DiHelper
    {
        /// <summary>
        /// Don't forget to register EventBus
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddTimeline(this IServiceCollection services)
        {
            services.AddSingleton<ITimelineCounter, TimelineCounter>();
            return services;
        }
    }
}
