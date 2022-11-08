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
        public void NotNullTest()
        {
            //Arrange
            var unityObject = new GameObject();
            var obj = new Object();

            //Act

            //Assert
            Assert.DoesNotThrow(() => unityObject.IsObjectNull());
            Assert.DoesNotThrow(() => obj.IsObjectNull());
        }

        [Test]
        public void UntiyNullAfterDestroyTest()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act
            DestroyImmediate(unityObject);

            //Assert
            Assert.Throws<InvalidOperationException>(() => unityObject.IsObjectNull());
        }

        [Test]
        public void ObjectNullTest()
        {
            //Arrange
            var obj = new Object();

            //Act

            obj = null;

            //Assert
            Assert.Throws<InvalidOperationException>(() => obj.IsObjectNull());
        }

        [Test]
        public void UnityNullTest()
        {
            //Arrange
            var unityObject = new GameObject();

            //Act

            unityObject = null;

            //Assert
            Assert.Throws<InvalidOperationException>(() => unityObject.IsObjectNull());
        }
    }
}
