using Microsoft.Extensions.DependencyInjection;

namespace AutSoft.UnitySupplements.Timeline
{
    public static class DiHelper
    {
        /// <summary>
        /// Don't forget to register EventBus
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTimeline(this IServiceCollection services)
        {
            services.AddSingleton<ITimelineCounter, TimelineCounter>();
            return services;
        }
    }
}
