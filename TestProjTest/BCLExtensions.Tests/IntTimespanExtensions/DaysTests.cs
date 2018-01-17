using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class DaysTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Days();

            Assert.Equal(5, result.TotalDays);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(265)]
        [InlineData(9001)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-265)]
        [InlineData(-9001)]
        [InlineData(-10675199)]
        [InlineData(10675199)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfDays)
        {
            var result = numberOfDays.Days();

            Assert.Equal(numberOfDays, result.TotalDays);
        }
    }
}
