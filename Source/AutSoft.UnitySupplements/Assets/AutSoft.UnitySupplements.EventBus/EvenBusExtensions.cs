using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;

namespace AutSoft.UnitySupplements.EventBus
{
    /// <summary>
    /// Extension methods for <see cref="IEventBus"/>.
    /// </summary>
    public static class EventBusExtensions
    {
        /// <summary>
        /// Adds an event subscription that is unsubscribed upon destroying the specified <paramref name="component"/>.
        /// </summary>
        /// <remarks>
        /// A new <see cref="DestroyDetector"/> component will be added to <paramref name="component"/> if not already present.
        /// </remarks>
        /// <param name="component">The component which will trigger <see cref="IEventBus.UnSubscribe{T}(Handler{T})"/> when destroyed.</param>
        /// <param name="handler">An event handler that is to be subscribed.</param>
        public static void SubscribeWeak<T>(this IEventBus eventBus, Component component, Handler<T> handler) where T : IEvent
        {
            eventBus.Subscribe(handler);
            component.gameObject.GetOrAddComponent<DestroyDetector>().Destroyed += () => eventBus.UnSubscribe(handler);
        }
    }
}
