using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ExtensionOverflowTests
{
	/// <summary>
	/// Test System.Int32 extension methods
	/// </summary>
	[TestClass]
	public class IntExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod]
		public void PercentageOfIntInt()
		{
			Assert.AreEqual(33.0M, 100.PercentageOf(33));
		}

		[TestMethod]
		public void PercentOfIntInt()
		{
			Assert.AreEqual(33.0M, 33.PercentOf(100));
		}

		[TestMethod]
		public void PercentageOfIntFloat()
		{
			Assert.AreEqual(33.0M, 100.PercentageOf((float)33.0F));
		}

		[TestMethod]
		public void PercentOfIntFloat()
		{
			Assert.AreEqual(33.0M, 33.PercentOf((float)100.0F));
		}

		[TestMethod]
		public void PercentageOfIntDouble()
		{
			Assert.AreEqual(33.0M, 100.PercentageOf((double)33.0F));
		}

		[TestMethod]
		public void PercentOfIntDouble()
		{
			Assert.AreEqual(33.0M, 33.PercentOf((double)100.0F));
		}

		[TestMethod]
		public void PercentageOfIntDecimal()
		{
			Assert.AreEqual(33.0M, 100.PercentageOf((decimal)33.0M));
		}

		[TestMethod]
		public void PercentOfIntDecimal()
		{
			Assert.AreEqual(33.0M, 33.PercentOf((decimal)100.0M));
		}

		[TestMethod]
		public void PercentageOfIntLong()
		{
			Assert.AreEqual(33.0M, 100.PercentageOf((long)33));
		}

		[TestMethod]
		public void PercentOfIntLong()
		{
			Assert.AreEqual(33.0M, 33.PercentOf((long)100));
		}
	}
}
