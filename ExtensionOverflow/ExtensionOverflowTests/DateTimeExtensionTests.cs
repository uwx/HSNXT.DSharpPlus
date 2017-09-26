using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionOverflow;

namespace TestProj47Tests
{
	/// <summary>
	/// Test System.DateTime extension methods.
	/// </summary>
	[TestClass]
	public class DateTimeExtensionTests
	{
        /// <summary>
        /// Provide information about current testing context.
        /// Required by MSTests.
        /// </summary>
        public TestContext TestContext { get; set; }

		[TestMethod]
		public void Elapset15min()
		{
            DateTime datetime = DateTime.Now.AddMinutes(-15);
			Assert.AreEqual(15, datetime.Elapsed().Minutes);
		}

        [TestMethod]
        public void Elapset15sec()
        {
            Assert.AreEqual(15, DateTime.Now.AddSeconds(-15).Elapsed().Seconds);
        }

        [TestMethod]
        public void ElapsetMinus15sec()
        {
            Assert.AreEqual(-15, DateTime.Now.AddSeconds(15).Elapsed().Seconds);
        }

        [TestMethod]
        public void FirstDateOfWeek()
        {
            DateTime date = new DateTime(2008, 11, 27);
            DateTime result = new DateTime(2008, 11, 24);
            Assert.AreEqual(result, date.FirstDateTimeOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(result, result.FirstDateTimeOfWeek(DayOfWeek.Monday));
            date = new DateTime(2008, 11, 30);
            result = date;
            Assert.AreEqual(result, date.FirstDateTimeOfWeek(DayOfWeek.Sunday));
        }

        [TestMethod]
        public void GetDateOfWeekFromDay()
        {
            DateTime date = new DateTime(2008, 11, 27);
            DateTime result = new DateTime(2008, 11, 25);
            Assert.AreEqual(result, date.GetDateTimeForDayOfWeek(DayOfWeek.Tuesday, DayOfWeek.Monday ));
            Assert.AreEqual(result, date.GetDateTimeForDayOfWeek(DayOfWeek.Tuesday, DayOfWeek.Sunday));
            date = new DateTime(2008, 11, 19);
            result = new DateTime(2008, 11, 25);
            Assert.AreEqual(result, date.GetDateTimeForDayOfWeek(DayOfWeek.Tuesday, DayOfWeek.Wednesday));
        }
	}
}
