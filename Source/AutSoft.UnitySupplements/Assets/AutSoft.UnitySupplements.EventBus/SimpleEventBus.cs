#nullable enable
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutSoft.UnitySupplements.EventBus
{
    public delegate void Handler<in T>(T message) where T : IEvent;

    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory) => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
    }

    public sealed class SimpleEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<object>> _handlers = new();
        private readonly ILogger<SimpleEventBus> _logger;

        private readonly ServiceFactory _factory;

        public SimpleEventBus(ServiceFactory factory,  ILogger<SimpleEventBus> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public void Subscribe<T>(Handler<T> handler) where T : IEvent
        {
            try
            {
                var eventType = typeof(T);
                if (_handlers.TryGetValue(eventType, out var handlers))
                {
                    handlers.Add(handler);
                }
                else
                {
                    _handlers.Add(eventType, new List<object> { handler });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Subscription failed for type {TypeName}", typeof(T).FullName);
            }
        }

        public void UnSubscribe<T>(Handler<T> handler) where T : IEvent
        {
            try
            {
                if (!_handlers.TryGetValue(typeof(T), out var handlers)) return;
                handlers.Remove(handler);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to unsubscribe from type {TypeName}", typeof(T).FullName);
            }
        }

        public void Invoke<T>(T item) where T : IEvent
        {
            foreach (var handler in _factory.GetInstances<IEventHandler<T>>())
            {
                try
                {
                    handler.Handle(item);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to invoke registered handler for type {TypeName} for message {Message}", typeof(T).FullName, item);
                }
            }

            foreach (var superType in _handlers.Keys.Where(k => k.IsAssignableFrom(typeof(T))).ToArray())
            {
                if (!_handlers.TryGetValue(superType, out var handlers)) continue;
                foreach (var handler in handlers.ToArray())
                {
                    try
                    {
                        ((Handler<T>)handler)(item);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to invoke subscribed handler for type {TypeName} for message {Message}", typeof(T).FullName, item);
                    }
                }
            }
        }
    }
}
