using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionOverflow;

namespace TestProj47Tests.Tests
{
    /// <summary>
    /// Summary description for StringExtensionTests
    /// </summary>
    [TestClass]
    public class NumericExtensionTests
    {
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests
        /// </summary>
        public TestContext TestContext { get; set; }

        #region ToPercent Tests
        /// <summary>
        /// Toes the percent simple test.
        /// </summary>
        [TestMethod]
        public void ToPercentSimpleTest()
        {
            decimal value = 23;
            decimal percentof = 100;
            decimal expected = 23;

            Assert.AreEqual(expected, value.ToPercent(percentof),
                "Percent of total 100 not correct calculated");
        }
        [TestMethod]
        public void ToPercentSimpleTest2()
        {
            decimal value = 23;
            decimal percentof = 200;
            decimal expected = 11.5M;

            Assert.AreEqual(expected, value.ToPercent(percentof),
                "Percent of total 200 not correct calculated");
        }
        #endregion
    }
}
