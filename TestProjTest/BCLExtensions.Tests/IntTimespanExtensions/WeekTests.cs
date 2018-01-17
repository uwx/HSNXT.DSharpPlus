using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class WeekTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Week();

            Assert.Equal(35, result.TotalDays);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 7)]
        [InlineData(5, 35)]
        [InlineData(10, 70)]
        [InlineData(265, 1855)]
        [InlineData(9001, 63007)]
        [InlineData(-1, -7)]
        [InlineData(-5, -35)]
        [InlineData(-10, -70)]
        [InlineData(-265, -1855)]
        [InlineData(-9001, -63007)]
        [InlineData(-1525028, -10675196)]
        [InlineData(1525028, 10675196)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfWeeks, int numberOfDays)
        {
            var result = numberOfWeeks.Week();

            Assert.Equal(numberOfDays, result.TotalDays);
        }

        [Theory]
        [InlineData(-1525029)]
        [InlineData(1525029)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void DayCountsOutsideOfTheMaximumLimitThrowAnArgumentOutOfRangeException(int numberOfDays)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => numberOfDays.Week());
        }
    }
}
