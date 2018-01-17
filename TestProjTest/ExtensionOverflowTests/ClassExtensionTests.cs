///----------------------------------------------------------------------------
///
/// ClassExtensionTests.cs - Extension Methods to be used on Classes.
///
/// Copyright (c) 2008 Extension Overflow
///
///----------------------------------------------------------------------------
///
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ExtensionOverflow.Tests
{
	public class DummyClass
	{
		public DummyClass()
		{

		}

		public string Name
		{
			get;
			set;
		}
	}

    /// <summary>
    /// Dummy class for testing purposes with circular references thrown into the mix.
    /// </summary>
    public class DummyCircularClass : DummyClass
    {
        public DummyCircularClass another;

        public DummyCircularClass() : base()
        {
            // create 2 more childrens to link in a circle
            var d1 = new DummyCircularClass(true);
            var d2 = new DummyCircularClass(true);

            // create circular references
            d1.another = this;
            this.another = d2;
            d2.another = this;
        }

        private DummyCircularClass(bool placeholder) : base()
        {
            /* does nothing */
        }
    }

    /// <summary>
    /// Tests System.Object extension methods.
    /// </summary>
	[TestClass]
	public class ClassExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void ThrowIfArgumentIsNullOnDummyClass()
		{
			DummyClass sut = null;

			sut.ThrowIfArgumentIsNull("DummyClass");			
		}

		[TestMethod]
		public void ThrowIfArgumentIsNotNullOnDummyClass()
		{
			var sut = new DummyClass();

			sut.ThrowIfArgumentIsNull("DummyClass");
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void ThrowIfArgumentIsNullOnString()
		{
			string sut = null;

			sut.ThrowIfArgumentIsNull("string");
		}

		[TestMethod]
		public void ThrowIfArgumentIsNotNullOnString()
		{
			var sut = "not null";

			sut.ThrowIfArgumentIsNull("string");
		}
	}
}
