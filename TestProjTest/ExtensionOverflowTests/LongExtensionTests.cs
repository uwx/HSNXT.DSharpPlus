using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ExtensionOverflowTests
{
	/// <summary>
	/// Test System.Int64 extension methods
	/// </summary>
	[TestClass]
	public class LongExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod]
		public void PercentageOfLongInt()
		{
			Assert.AreEqual(33.0M, ((long)100).PercentageOf((int)33));
		}

		[TestMethod]
		public void PercentOfLongInt()
		{
			Assert.AreEqual(33.0M, ((long)33).PercentOf((int)100));
		}

		[TestMethod]
		public void PercentageOfLongFloat()
		{
			Assert.AreEqual(33.0M, ((long)100).PercentageOf((float)33.0F));
		}

		[TestMethod]
		public void PercentOfLongFloat()
		{
			Assert.AreEqual(33.0M, ((long)33).PercentOf((float)100.0F));
		}

		[TestMethod]
		public void PercentageOfLongDouble()
		{
			Assert.AreEqual(33.0M, ((long)100).PercentageOf((double)33.0F));
		}

		[TestMethod]
		public void PercentOfLongDouble()
		{
			Assert.AreEqual(33.0M, ((long)33).PercentOf((double)100.0F));
		}

		[TestMethod]
		public void PercentageOfLongDecimal()
		{
			Assert.AreEqual(33.0M, ((long)100).PercentageOf((decimal)33.0M));
		}

		[TestMethod]
		public void PercentOfLongDecimal()
		{
			Assert.AreEqual(33.0M, ((long)33).PercentOf((decimal)100.0M));
		}

		[TestMethod]
		public void PercentageOfLongLong()
		{
			Assert.AreEqual(33.0M, ((long)100).PercentageOf((long)33));
		}

		[TestMethod]
		public void PercentOfLongLong()
		{
			Assert.AreEqual(33.0M, ((long)33).PercentOf((long)100));
		}
	}
}
