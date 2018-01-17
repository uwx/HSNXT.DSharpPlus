using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class WeeksTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Weeks();

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
            var result = numberOfWeeks.Weeks();

            Assert.Equal(numberOfDays, result.TotalDays);
        }
    }
}
