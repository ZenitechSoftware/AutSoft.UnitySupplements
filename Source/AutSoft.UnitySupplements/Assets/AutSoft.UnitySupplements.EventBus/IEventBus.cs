#nullable enable
namespace AutSoft.UnitySupplements.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T>(Handler<T> handler) where T : IEvent;
        void UnSubscribe<T>(Handler<T> handler) where T : IEvent;
        void Invoke<T>(T item) where T : IEvent;
    }
}
