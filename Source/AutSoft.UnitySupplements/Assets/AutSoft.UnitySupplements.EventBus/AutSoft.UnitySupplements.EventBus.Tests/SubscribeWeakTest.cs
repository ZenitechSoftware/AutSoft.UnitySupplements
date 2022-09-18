#nullable enable
using Injecter;
using Injecter.Unity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.Unity3D;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace AutSoft.UnitySupplements.EventBus.Tests
{
    public class SubscribeWeakTest
    {
        private IEventBus _eventBus = default!;
        private EventHandlerCounter _counter = default!;

        [SetUp]
        public void Init()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEventBus();
            serviceCollection.AddLogging(builder => builder.AddSerilog(new LoggerConfiguration().WriteTo.Unity3D().CreateLogger()));
            serviceCollection.AddSingleton<EventHandlerCounter>();
            serviceCollection.AddInjecter(o => o.UseCaching = true);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            CompositionRoot.ServiceProvider = serviceProvider;

            _eventBus = serviceProvider.GetRequiredService<IEventBus>();
            _counter = serviceProvider.GetRequiredService<EventHandlerCounter>();
        }
        [UnityTest]
        public IEnumerator Test()
        {
            var testObject = new GameObject("TestObject");
            testObject.AddComponent<TestComponent>();
            testObject.AddComponent<MonoInjector>();
            yield return null;

            _eventBus.Invoke(new BaseEvent());
            Assert.AreEqual(1, _counter.BaseCalled);
            Object.Destroy(testObject);
            yield return null;

            _eventBus.Invoke(new BaseEvent());
            Assert.AreEqual(1, _counter.BaseCalled);
            yield return null;
        }
        private class BaseEvent : IEvent { }

        private class TestComponent : MonoBehaviour
        {
            [Inject] private readonly IEventBus _eventBus = default!;
            [Inject] private readonly EventHandlerCounter _counter = default!;

            private void Start() => _eventBus.SubscribeWeak<BaseEvent>(this, OnBaseCalled);

            private void OnBaseCalled(BaseEvent message) => _counter.BaseCalled++;
        }

        private class EventHandlerCounter
        {
            public int BaseCalled { get; set; }
            public int DerivedCalled { get; set; }
        }
    }
}
