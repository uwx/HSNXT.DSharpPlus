using System;
using Xunit;
using XunitShould;

namespace HSNXT.Tests
{
    public class DateTimeExtensionsTestFixture
    {
        [Fact]
        public void DateTimeIsBetweenReturnsTrueForValidCase()
        {
            var start = new DateTime(2000, 01, 01);
            var end = new DateTime(2010, 01, 01);
            var between = new DateTime(2005, 01, 01);

            var result = between.IsBetween(start, end);

            result.ShouldEqual(true);
        }

        [Fact]
        public void DateTimeIsBetweenReturnsFalseForValidCase()
        {
            var start = new DateTime(2000, 01, 01);
            var end = new DateTime(2010, 01, 01);
            var between = new DateTime(2012, 01, 01);

            var result = between.IsBetween(start, end);

            result.ShouldEqual(false);
        }

        [Fact]
        public void DateTimeIsBetweenReturnsTrueForEdgeCase()
        {
            var start = new DateTime(2000, 01, 01);
            var end = new DateTime(2010, 01, 01);
            var between = new DateTime(2010, 01, 01);

            var result = between.IsBetween(start, end, true);

            result.ShouldEqual(true);
        }

        [Fact]
        public void DateTimeIsBetweenReturnsFalseForEdgeCase()
        {
            var start = new DateTime(2000, 01, 01);
            var end = new DateTime(2010, 01, 01);
            var between = new DateTime(2010, 01, 01);

            var result = between.IsBetween(start, end, false);

            result.ShouldEqual(false);
        }

        [Fact]
        public void GetFinancialYearStartDateGetsCorrectYearInJanToJune()
        {
            var date = new DateTime(2010, 01, 01);
            var start = date.GetFinancialYearStartDate();
            start.Year.ShouldEqual(2009);
            start.Month.ShouldEqual(7);
            start.Day.ShouldEqual(1);
        }

        [Fact]
        public void GetFinancialYearStartYearGetsCorrectYearInJulyToDec()
        {
            var date = new DateTime(2010, 11, 01);
            var end = date.GetFinancialYearStartDate();
            end.Year.ShouldEqual(2010);
            end.Month.ShouldEqual(7);
            end.Day.ShouldEqual(1);
        }

        [Fact]
        public void EndOfDayFunctionCorrectlyReturnsRightTime()
        {
            var date = new DateTime(2010, 10, 10, 10, 10, 10);
            var endOfDay = new DateTime(2010, 10, 10, 23, 59, 59, 999);
            var result = date.EndOfDay();
            result.ShouldEqual(endOfDay);
        }
    }
}
