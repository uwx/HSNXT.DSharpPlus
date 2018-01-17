using System;
using NUnit.Framework;
using HSNXT.ComLib;


namespace CommonLibrary.Tests
{
    

    [TestFixture]
    public class TimeParserTests
    {
        [Test]
        public void CanNotParse()
        {
            var result = TimeHelper.Parse("sf");
            Assert.IsFalse(result.Success);
        }


        [Test]
        public void CanNotParseWithOutAmPm()
        {
            var time = TimeHelper.Parse("1").Item;
            Assert.AreEqual(time.Hours, 1);
            Assert.AreEqual(time.Minutes, 0);           
        }


        [Test]
        public void CanParseWithoutMinutesInPm()
        {
            var time = TimeHelper.Parse("1pm").Item;
            Assert.AreEqual(time.Hours, 13);
            Assert.AreEqual(time.Minutes, 0);       
        }


        [Test]
        public void CanParseWithoutMinutesInAm()
        {
            var time = TimeHelper.Parse("8am").Item;
            Assert.AreEqual(time.Hours, 8);
            Assert.AreEqual(time.Minutes, 0);       
        }


        [Test]
        public void CanParseWithMinutesInPm()
        {
            var time = TimeHelper.Parse("10:45pm").Item;
            Assert.AreEqual(time.Hours, 22);
            Assert.AreEqual(time.Minutes, 45);       
        }


        [Test]
        public void CanParseWithMinutesInAm()
        {
            var time = TimeHelper.Parse("6:30am").Item;
            Assert.AreEqual(time.Hours, 6);
            Assert.AreEqual(time.Minutes, 30);       
        }


        [Test]
        public void CanParseTimeRange()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30am to 1pm");

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);           
        }


        [Test]
        public void CanParseTimeRangeWithPeriodsInAmPm()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30 a.m. to 1 p.m.");

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);
        }


        [Test]
        public void CanParseTimeRangeWithSpacesBetweenTimeAndAm()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30 am to 1 pm");

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);
        }


        [Test]
        public void CanParseTimeRangeWithOutEndTime()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30am");

            var start = new TimeSpan(11, 30, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
            Assert.AreEqual(TimeParserConstants.MaxDate, result.EndTimeAsDate);           
        }


        [Test]
        public void CanParseIndividualStartEndTimes()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30am", "1pm", true);

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);   
        }


        [Test]
        public void CanNotParseIndividualStartEndTimesWithMissingEndTime()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30am", "", true);

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(TimeSpan.MinValue, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
        }


        [Test]
        public void CanParseIndividualStartEndTimesWithMissingEndTime()
        {
            var result = TimeHelper.ParseStartEndTimes("11:30am", "", false);

            var start = new TimeSpan(11, 30, 0);
            var end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
        }
    }
}
