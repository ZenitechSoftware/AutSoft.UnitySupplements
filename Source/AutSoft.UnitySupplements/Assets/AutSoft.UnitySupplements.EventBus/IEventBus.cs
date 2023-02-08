#nullable enable
namespace AutSoft.UnitySupplements.EventBus
{
    /// <summary>
    /// Common interface for custom EventBus implementations.
    /// Use this if you want to implement your own event bus. See <see cref="SimpleEventBus"/> for a simple example.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes a handler for event <typeparamref name="T"/>.
        /// </summary>
        void Subscribe<T>(Handler<T> handler) where T : IEvent;

        /// <summary>
        /// Unsubscribes a handler for event <typeparamref name="T"/>.
        /// </summary>
        void UnSubscribe<T>(Handler<T> handler) where T : IEvent;

        /// <summary>
        /// Invokes all subscribed <see cref="IEventHandler{T}"/> for event <typeparamref name="T"/>.
        /// </summary>
        void Invoke<T>(T item) where T : IEvent;
    }
}
