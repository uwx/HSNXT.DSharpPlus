using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ExtensionOverflowTests
{
	/// <summary>
	/// Test System.Double extension methods.
	/// </summary>
	[TestClass]
	public class DoubleExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod]
		public void PercentageOfDoubleInt()
		{
			Assert.AreEqual(33.0M, ((double)100.0F).PercentageOf((int)33));
		}

		[TestMethod]
		public void PercentOfDoubleInt()
		{
			Assert.AreEqual(33.0M, ((double)33.0F).PercentOf((int)100));
		}

		[TestMethod]
		public void PercentageOfDoubleFloat()
		{
			Assert.AreEqual(33.0M, ((double)100.0F).PercentageOf((float)33.0F));
		}

		[TestMethod]
		public void PercentOfDoubleFloat()
		{
			Assert.AreEqual(33.0M, ((double)33.0F).PercentOf((float)100.0F));
		}

		[TestMethod]
		public void PercentageOfDoubleDouble()
		{
			Assert.AreEqual(33.0M, ((double)100.0F).PercentageOf((double)33.0F));
		}

		[TestMethod]
		public void PercentOfDoubleDouble()
		{
			Assert.AreEqual(33.0M, ((double)33.0F).PercentOf((double)100.0F));
		}

		[TestMethod]
		public void PercentageOfDoubleLong()
		{
			Assert.AreEqual(33.0M, ((double)100.0F).PercentageOf((long)33));
		}

		[TestMethod]
		public void PercentOfDoubleLong()
		{
			Assert.AreEqual(33.0M, ((double)33.0F).PercentOf((long)100));
		}
	}
}
