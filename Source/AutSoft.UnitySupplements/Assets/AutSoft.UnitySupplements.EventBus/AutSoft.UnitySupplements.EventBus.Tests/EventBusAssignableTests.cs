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

        private class BaseHandlerEvent : IEvent
        {
            public int Called { get; set; }
            public BaseHandlerEvent() => Called = 0;
        }
        private class DerivedHandlerEvent : BaseHandlerEvent { }

        private IEventBus _eventBus = default!;
        private int _baseCalled;
        private int _derivedCalled;

        [SetUp]
        public void Init()
        {
            _baseCalled = 0;
            _derivedCalled = 0;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEventBus(typeof(EventBusAssignableTests).Assembly);
            serviceCollection.AddLogging(builder => builder.AddSerilog(new LoggerConfiguration().WriteTo.Unity3D().CreateLogger()));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _eventBus = serviceProvider.GetRequiredService<IEventBus>();
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

        [Test]
        public void EventHandlerWork()
        {
            var baseEvent = new BaseHandlerEvent();
            _eventBus.Invoke(baseEvent);

            Assert.AreEqual(1, baseEvent.Called);
        }

        [Test]
        public void DerivedEventHandlerWork()
        {
            var baseEvent = new BaseHandlerEvent();

            var derBaseHandlerEvent = new BaseHandlerEvent();
            _eventBus.Invoke(baseEvent);

            Assert.AreEqual(1, baseEvent.Called);
            Assert.AreEqual(0, derBaseHandlerEvent.Called);

            _eventBus.Invoke(derBaseHandlerEvent);

            Assert.AreEqual(2, baseEvent.Called);
            Assert.AreEqual(1, derBaseHandlerEvent.Called);
        }

        private void OnDerivedCalled(DerivedEvent message) => _derivedCalled++;

        private void OnBaseCalled(BaseEvent message) => _baseCalled++;

        private class BaseTestEventHandler : IEventHandler<BaseHandlerEvent>
        {
            public void Handle(BaseHandlerEvent message) => message.Called++;
        }

        private class DerivedTestEventHandler : IEventHandler<DerivedHandlerEvent>
        {
            public void Handle(DerivedHandlerEvent message) => message.Called++;
        }
    }
}
