using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HSNXT;

namespace ExtensionOverflowTests
{
    
    
    /// <summary>
    ///This is a test class for LinqExtensionsTest and is intended
    ///to contain all LinqExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class LinqExtensionsTest
    {
        public TestContext TestContext { get; set; }


        /// <summary>
        /// Test that the ForEach(Action) method executes the supplied action on all elements.
        /// </summary>
        [TestMethod]
        public void ForEachAction()
        {
            var @enum = new[] {1, 2, 3, 4}.AsEnumerable();
            var sum = 0;

            @enum.ForEach(n => sum += n);

            Assert.AreEqual(sum, @enum.Sum());
        }

        /// <summary>
        /// Test that the ForEach(Func) method executes the supplied function on all elements.
        /// </summary>
        [TestMethod]
        public void ForEachFunc()
        {
            var @enum = new[] {1, 2, 3, 4}.AsEnumerable();
            var sum = 0;

            @enum.ForEach(n => sum += n);

            Assert.AreEqual(sum, @enum.Sum());
        }
    }
}
