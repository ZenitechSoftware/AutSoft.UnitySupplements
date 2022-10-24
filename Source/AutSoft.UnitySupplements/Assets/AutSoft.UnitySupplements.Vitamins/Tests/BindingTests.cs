using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

namespace AutSoft.UnitySupplements.Vitamins.Tests
{
    public class BindingTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void BindingTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BindingTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
