using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;

namespace AutSoft.UnitySupplements.EventBus
{
    public static class EventBusExtensions
    {
        public static void SubscribeWeak<T>(this IEventBus eventBus, Component component, Handler<T> handler) where T : IEvent
        {
            eventBus.Subscribe(handler);
            component.gameObject.GetOrAddComponent<DestroyDetector>().Destroyed += () => eventBus.UnSubscribe(handler);
        }
    }
}
