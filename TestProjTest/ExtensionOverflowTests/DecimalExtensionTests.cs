using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ExtensionOverflowTests
{
	/// <summary>
	/// Test System.Decimal extension methods.
	/// </summary>
	[TestClass]
	public class DecimalExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod]
		public void PercentageOfDecimalInt()
		{
			Assert.AreEqual(33.0M, 100.0M.PercentageOf((int)33));
		}

		[TestMethod]
		public void PercentOfDecimalInt()
		{
			Assert.AreEqual(33.0M, 33.0M.PercentOf((int)100));			
		}

		[TestMethod]
		public void PercentageOfDecimalDecimal()
		{
			Assert.AreEqual(33.3M, 100.0M.PercentageOf(33.3M));
		}

		[TestMethod]
		public void PercentOfDecimalDecimal()
		{
			Assert.AreEqual(33.3M, 33.3M.PercentOf(100.0M));
		}


		[TestMethod]
		public void PercentageOfDecimalLong()
		{
			Assert.AreEqual(200.0M, 100.0M.PercentageOf((long)200));
		}

		[TestMethod]
		public void PercentOfDecimalLong()
		{
			Assert.AreEqual(200.0M, 200.0M.PercentOf((long)100));
		}
	}
}
