#nullable enable
namespace AutSoft.UnitySupplements.EventBus
{
    public interface IEventHandler<in T> where T : IEvent
    {
        void Handle(T message);
    }
}
