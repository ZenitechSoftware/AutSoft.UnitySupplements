using AutSoft.UnitySupplements.Vitamins.Bindings;
using CommunityToolkit.Mvvm.ComponentModel;
using NUnit.Framework;
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.Tests
{
    public class BindingTests
    {
        [Test]
        public void One_Way_Binding_Sets_Initial_Value()
        {
            // Arrange
            var component = new GameObject().AddComponent<OneWayComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.Setup(vm);

            // Assert
            Assert.AreEqual("1", vm.Message);
        }

        [Test]
        public void One_Way_Binding_Updates()
        {
            // Arrange
            var component = new GameObject().AddComponent<OneWayComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.Setup(vm);
            vm.Message = "2";

            // Assert
            Assert.AreEqual("2", vm.Message);
        }

        [Test]
        public void One_Way_Destroy_Ends_Binding()
        {
            // Arrange
            var component = new GameObject().AddComponent<OneWayComponent>();
            var vm = new BindingViewModel() { Message = "1" };

            // Act
            component.Setup(vm);
            vm.Message = "2";

            UnityEngine.Object.DestroyImmediate(component.gameObject, true);

            // Assert
            Assert.DoesNotThrow(() => vm.Message = "3");
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

    public sealed class OneWayComponent : MonoBehaviour
    {
        private bool _isDestroyed = false;

        public string Message { get; set; }

        public void Setup(BindingViewModel viewModel) =>
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

        private void OnDestroy() => _isDestroyed = true;
    }
}
