#nullable enable
namespace AutSoft.UnitySupplements.EventBus
{
    /// <summary>
    /// Implement this interface to handle events invoked through <see cref="IEventBus"/>.
    /// </summary>
    /// <typeparam name="T">The event type to be handled.</typeparam>
    public interface IEventHandler<in T> where T : IEvent
    {
        void Handle(T message);
    }
}
