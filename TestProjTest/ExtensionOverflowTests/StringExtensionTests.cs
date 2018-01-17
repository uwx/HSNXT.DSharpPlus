using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using HSNXT;

namespace ExtensionOverflow.Tests
{
    /// <summary>
    /// Test System.String extension methods.
    /// </summary>
    [TestClass]
    public class StringExtensionTests
    {
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		#region FormatWith

		[TestMethod]
        public void FormatWithStringOneArgument()
        {
            var s = "{0} ought to be enough for everybody.";
            var param0 = "64K";

            var expected = "64K ought to be enough for everybody.";

            Assert.AreEqual(expected, s.FormatWith(param0),
                "1-argument string.FormatWith is not formatting string properly.");
        }

		[TestMethod]
		public void FormatWithStringOneNullArgument()
		{
			var s = "{0} ought to be enough for everybody.";
			string param0 = null;

			var expected = " ought to be enough for everybody.";

			Assert.AreEqual(expected, s.FormatWith(param0),
				"1-argument string.FormatWith is not formatting string properly.");
		}

        [TestMethod]
        public void FormatWithStringTwoArguments()
        {
            var s = "{0} ought to be enough for {1}.";
            var param0 = "64K";
            var param1 = "everybody";

            var expected = "64K ought to be enough for everybody.";

            Assert.AreEqual(expected, s.FormatWith(param0, param1),
                "2-arguments string.FormatWith is not formatting string properly.");
        }

		[TestMethod]
		public void FormatWithStringTwoNullArguments()
		{
			var s = "{0} ought to be enough for {1}.";
			string param0 = null;
			string param1 = null;

			var expected = " ought to be enough for .";

			Assert.AreEqual(expected, s.FormatWith(param0, param1),
				"2-arguments string.FormatWith is not formatting string properly.");
		}

        [TestMethod]
        public void FormatWithStringThreeArguments()
        {
            var s = "{0} ought to be {1} for {2}.";
            var param0 = "64K";
            var param1 = "enough";
            var param2 = "everybody";

            var expected = "64K ought to be enough for everybody.";

            Assert.AreEqual(expected, s.FormatWith(param0, param1, param2),
                "3-arguments string.FormatWith is not formatting string properly.");
        }

		[TestMethod]
		public void FormatWithStringThreeNullArguments()
		{
			var s = "{0} ought to be {1} for {2}.";
			string param0 = null;
			string param1 = null;
			string param2 = null;

			var expected = " ought to be  for .";

			Assert.AreEqual(expected, s.FormatWith(param0, param1, param2),
				"3-arguments string.FormatWith is not formatting string properly.");
		}

        [TestMethod]
        public void FormatWithStringMultipleArguments()
        {
            var s = "{0} {1} to be {2} for {3}.";
            var param0 = "64K";
            var param1 = "ought";
            var param2 = "enough";
            var param3 = "everybody";

            var expected = "64K ought to be enough for everybody.";

            Assert.AreEqual(expected, s.FormatWith(param0, param1, param2, param3),
                "4-arguments string.FormatWith is not formatting string properly.");
        }

		[TestMethod]
		public void FormatWithStringMultipleNullArguments()
		{
			var s = "{0} {1} to be {2} for {3}.";
			string param0 = null;
			string param1 = null;
			string param2 = null;
			string param3 = null;

			var expected = "  to be  for .";

			Assert.AreEqual(expected, s.FormatWith(param0, param1, param2, param3),
				"4-arguments string.FormatWith is not formatting string properly.");
		}

        [TestMethod]
        public void FormatWithStringObeyThreadCulture()
        {
            var s = "{0} is used as the decimal separator in French, as in {1:#,##0.00}";
            var param0 = "Comma";
            var param1 = 123.45F;

            var expected = "Comma is used as the decimal separator in French, as in 123,45";

            // save current culture for later restore and switch to fr-FR culture
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

			Assert.AreEqual(expected, s.FormatWith(Thread.CurrentThread.CurrentCulture, param0, param1),
                "2-arguments string.FormatWith does not obey the current thread culture.");

            // restore culture
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

		#endregion

        #region Conversions

        private enum DummyEnum
        { 
            one,
            two,
            three
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyStringToDummyEnum()
        {
            "".ToEnum<DummyClass>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullStringToDummyEnum()
        {
            string nullstring = null;
            nullstring.ToEnum<DummyClass>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Type provided must be an Enum.")]
        public void StringToNonEnumType() 
        {
            "dummystring".ToEnum<DummyClass>();
        }

        [TestMethod]
        public void StringToDummyEnum()
        {
            Assert.AreEqual(DummyEnum.one, "one".ToEnum<DummyEnum>());
        }

        [TestMethod]
        public void IgnoreCaseStringToDummyEnum()
        {
            Assert.AreEqual(DummyEnum.one, "one".ToEnum<DummyEnum>(true));
        }

        [TestMethod]
        public void DoubleStringToInt()
        {
            Assert.AreEqual(88, (88.45).ToString().ToInteger(), "Could not convert " + (88.45).ToString() + " to 88");
        }

        [TestMethod]
        public void IntStringToInt()
        {
            Assert.AreEqual(22, "22".ToInteger(), "Could not convert 22 to 22");
        }
        
        [TestMethod]
        public void ABCStringToInt()
        {
            Assert.AreEqual(0, "ABC".ToInteger());
        }

        [TestMethod]
        public void DoubleStringToDouble()
        {
            Assert.AreEqual(88.45, (88.45).ToString().ToDouble(), "Could not convert " + (88.45).ToString() + " to 88.45");
        }

        [TestMethod]
        public void ABCStringToDouble()
        {
            Assert.AreEqual(0, "ABC".ToDouble());
        }

        [TestMethod]
        public void DateTimeStringToDateTime()
        {
            var date = new DateTime(2000,1,1,13,12,11);
            Assert.AreEqual(date, date.ToString().ToDateTime());
        }

        [TestMethod]
        public void DateStringToDateTime()
        {
            var date = DateTime.Now;
            Assert.AreEqual(date.Date, date.Date.ToString(CultureInfo.InvariantCulture).ToDateTime());
        }

        [TestMethod]
        public void ABCToDateTime()
        {
            Assert.ThrowsException<FormatException>(() => "ABC".ToDateTime());
        }

        [TestMethod]
        public void TStringToBoolean()
        {
            Assert.AreEqual(true, "T".ToBoolean());
        }

        [TestMethod]
        public void FStringToBoolean()
        {
            Assert.AreEqual(false, "F".ToBoolean());
        }

        [TestMethod]
        public void tStringToBoolean()
        {
            Assert.AreEqual(true, "t".ToBoolean());
        }

        [TestMethod]
        public void fStringToBoolean()
        {
            Assert.AreEqual(false, "f".ToBoolean());
        }

        [TestMethod]
        public void TrueStringToBoolean()
        {
            Assert.AreEqual(true, "True".ToBoolean());
        }

        [TestMethod]
        public void FalseStringToBoolean()
        {
            Assert.AreEqual(false, "False".ToBoolean());
        }

        [TestMethod]
        public void ABCToBoolean()
        {
            Assert.AreEqual(null, "ABC".ToBoolean());
        }
        #endregion

        #region ValueOrDefault
        [TestMethod]
        public void NullStringToDefault()
        {
            string value = null;
            Assert.AreEqual("Test", value.GetValueOrDefault("Test"));
        }
        [TestMethod]
        public void NullStringToEmpty()
        {
            string value = null;
            Assert.AreEqual("", value.GetValueOrEmpty());
        }
        #endregion
    }
}
