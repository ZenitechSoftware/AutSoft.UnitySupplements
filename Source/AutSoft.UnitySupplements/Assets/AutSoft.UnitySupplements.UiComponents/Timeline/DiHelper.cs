#nullable enable
using Microsoft.Extensions.DependencyInjection;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to add Timeline functionalities.
    /// </summary>
    public static class DiHelper
    {
        /// <summary>
        /// Adds <see cref="ITimelineCounter"/> implemented by <see cref="TimelineCounter"/> as a singleton service.
        /// </summary>
        /// <remarks>
        /// <see cref="TimelineCounter"/> depends on <see cref="EventBus.IEventBus"/>
        /// </remarks>
        public static IServiceCollection AddTimeline(this IServiceCollection services)
        {
            services.AddSingleton<ITimelineCounter, TimelineCounter>();
            return services;
        }
    }
}
