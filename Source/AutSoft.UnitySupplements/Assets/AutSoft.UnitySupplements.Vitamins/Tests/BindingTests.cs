using AutSoft.UnitySupplements.Vitamins.Bindings;
using CommunityToolkit.Mvvm.ComponentModel;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.Vitamins.Tests
{
    public class BindingTests
    {
        [Test]
        public void One_Way_Binding_Sets_Initial_Value()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindOneWay(vm);

            // Assert
            Assert.AreEqual("1", component.Message);
        }

        [Test]
        public void One_Way_Binding_Updates()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindOneWay(vm);
            vm.Message = "2";

            // Assert
            Assert.AreEqual("2", component.Message);
        }

        [Test]
        public void One_Way_Destroy_Ends_Binding()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindOneWay(vm);
            vm.Message = "2";

            UnityEngine.Object.DestroyImmediate(component.gameObject, true);

            // Assert
            Assert.DoesNotThrow(() => vm.Message = "3");
        }

        [Test]
        public void Two_Way_Binding_Sets_Initial_Value()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindTwoWay(vm);

            // Assert
            Assert.AreEqual("1", component.Message);
        }

        [Test]
        public void Two_Way_Binding_Updates_FromSource()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindTwoWay(vm);
            vm.Message = "2";

            // Assert
            Assert.AreEqual("2", component.Message);
        }

        [Test]
        public void Two_Way_Binding_Updates_FromTarget()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindTwoWay(vm);
            component.ChangeMessage("2");

            // Assert
            Assert.AreEqual("2", vm.Message);
        }

        [Test]
        public void Two_Way_Destroy_Ends_Binding()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.BindTwoWay(vm);
            vm.Message = "2";

            UnityEngine.Object.DestroyImmediate(component.gameObject, true);

            // Assert
            Assert.DoesNotThrow(() => vm.Message = "3");
        }

        [Test]
        public void Binding_Lifetime_Is_Respected()
        {
            // Arrange
            var component = new GameObject().AddComponent<TestBindingComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            using (var _ = component.BindTwoWay(vm))
            {
                component.ChangeMessage("2");
            }

            vm.Message = "3";

            // Assert
            Assert.AreEqual("2", component.Message);
        }
    }

    public sealed class BindingViewModel : ObservableObject
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }

    public sealed class TestBindingComponent : MonoBehaviour
    {
        private bool _isDestroyed = false;

        private readonly UnityEvent<string> _messageEvent = new();
        public string Message { get; set; }

        public BindingLifetime BindOneWay(BindingViewModel viewModel) =>
            this.Bind
            (
                viewModel,
                x => x.Message,
                m =>
                {
                    if (_isDestroyed) throw new InvalidOperationException();
                    Message = m;
                }
            );

        public BindingLifetime BindTwoWay(BindingViewModel viewModel) =>
            this.Bind
            (
                viewModel,
                x => x.Message,
                m =>
                {
                    if (_isDestroyed) throw new InvalidOperationException();
                    Message = m;
                },
                _messageEvent,
                x => x
            );

        public void ChangeMessage(string message) => _messageEvent.Invoke(message);

        private void OnDestroy() => _isDestroyed = true;
    }
}
