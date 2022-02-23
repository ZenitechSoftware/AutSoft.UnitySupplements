#nullable enable
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.Unity3D;

namespace AutSoft.UnitySupplements.EventBus.AutSoft.UnitySupplements.EventBus.Tests
{
    [TestFixture]
    public class EventBusAssignableTests
    {
        private class BaseEvent : IEvent { }
        private class DerivedEvent : BaseEvent { }

        private IEventBus _eventBus = default!;
        private int _baseCalled;
        private int _derivedCalled;
        private EventHandlerCounter _counter;

        [SetUp]
        public void Init()
        {
            _baseCalled = 0;
            _derivedCalled = 0;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEventBus(typeof(EventBusAssignableTests).Assembly);
            serviceCollection.AddLogging(builder => builder.AddSerilog(new LoggerConfiguration().WriteTo.Unity3D().CreateLogger()));
            serviceCollection.AddSingleton<EventHandlerCounter>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _eventBus = serviceProvider.GetRequiredService<IEventBus>();
            _counter = serviceProvider.GetRequiredService<EventHandlerCounter>();
        }

        [Test]
        public void EventsWork()
        {
            _eventBus.Subscribe<BaseEvent>(OnBaseCalled);

            var baseEvent = new BaseEvent();
            _eventBus.Invoke(baseEvent);
            Assert.AreEqual(1, _baseCalled);

            _eventBus.UnSubscribe<BaseEvent>(OnBaseCalled);
        }

        [Test]
        public void DerivedEventsWork()
        {
            _eventBus.Subscribe<BaseEvent>(OnBaseCalled);
            _eventBus.Subscribe<DerivedEvent>(OnDerivedCalled);

            var baseEvent = new BaseEvent();
            var derivedEvent = new DerivedEvent();

            _eventBus.Invoke(baseEvent);

            Assert.AreEqual(1, _baseCalled);
            Assert.AreEqual(0, _derivedCalled);

            _eventBus.Invoke(derivedEvent);

            Assert.AreEqual(2, _baseCalled);
            Assert.AreEqual(1, _derivedCalled);

            _eventBus.UnSubscribe<BaseEvent>(OnBaseCalled);
            _eventBus.UnSubscribe<DerivedEvent>(OnDerivedCalled);
        }

        private void OnDerivedCalled(DerivedEvent message) => _derivedCalled++;
        private void OnBaseCalled(BaseEvent message) => _baseCalled++;

        [Test]
        public void EventHandlerWork()
        {
            _eventBus.Invoke(new BaseEvent());

            Assert.AreEqual(1, _counter.BaseCalled);
        }

        [Test]
        public void DerivedEventHandlerWork()
        {
            _eventBus.Invoke(new BaseEvent());

            Assert.AreEqual(1, _counter.BaseCalled);
            Assert.AreEqual(0, _counter.DerivedCalled);

            _eventBus.Invoke(new DerivedEvent());

            Assert.AreEqual(2, _counter.BaseCalled);
            Assert.AreEqual(1, _counter.DerivedCalled);
        }

        private class BaseTestEventHandler<T> : IEventHandler<T> where T : BaseEvent
        {
            private readonly EventHandlerCounter _counter;

            public BaseTestEventHandler(EventHandlerCounter counter) => _counter = counter;

            public void Handle(T message) => _counter.BaseCalled++;
        }

        private class DerivedTestEventHandler : IEventHandler<DerivedEvent>
        {
            private readonly EventHandlerCounter _counter;

            public DerivedTestEventHandler(EventHandlerCounter counter) => _counter = counter;
            public void Handle(DerivedEvent message) => _counter.DerivedCalled++;
        }

        private class EventHandlerCounter
        {
            public int BaseCalled { get; set; }
            public int DerivedCalled { get; set; }
        }
    }
}
