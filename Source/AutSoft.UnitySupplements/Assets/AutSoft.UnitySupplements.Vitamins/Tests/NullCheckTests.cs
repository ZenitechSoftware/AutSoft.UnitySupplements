using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using UnityEngine;
using Object = System.Object;

namespace AutSoft.UnitySupplements.Vitamins.Tests
{
    public class NullCheckTests : MonoBehaviour
    {
        [Test]
        public void NotNullTestThrow()
        {
            //Arrange
            var unityObject = new GameObject();
            var obj = new Object();

            //Act

            //Assert
            Assert.DoesNotThrow(() => unityObject.IsObjectNullThrow());
            Assert.DoesNotThrow(() => obj.IsObjectNullThrow());
        }

        [Test]
        public void UntiyNullAfterDestroyTestThrow()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act
            DestroyImmediate(unityObject);

            //Assert
            Assert.Throws<InvalidOperationException>(() => unityObject.IsObjectNullThrow());
        }

        [Test]
        public void ObjectNullTestThrow()
        {
            //Arrange
            var obj = new Object();

            //Act

            obj = null;

            //Assert
            Assert.Throws<InvalidOperationException>(() => obj.IsObjectNullThrow());
        }

        [Test]
        public void UnityNullTestThrow()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act

            unityObject = null;

            //Assert
            Assert.Throws<InvalidOperationException>(() => unityObject.IsObjectNullThrow());
        }

        [Test]
        public void NotNullTest()
        {
            //Arrange
            var unityObject = new GameObject();
            var obj = new Object();

            //Act

            //Assert
            Assert.False(unityObject.IsObjectNull());
            Assert.False(obj.IsObjectNull());
        }

        [Test]
        public void UntiyNullAfterDestroyTest()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act
            DestroyImmediate(unityObject);

            //Assert
            Assert.True(unityObject.IsObjectNull());
        }

        [Test]
        public void ObjectNullTest()
        {
            //Arrange
            var obj = new Object();

            //Act

            obj = null;

            //Assert
            Assert.True(obj.IsObjectNull());
        }

        [Test]
        public void UnityNullTest()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act

            unityObject = null;

            //Assert
            Assert.True(unityObject.IsObjectNull());
        }
    }
}
