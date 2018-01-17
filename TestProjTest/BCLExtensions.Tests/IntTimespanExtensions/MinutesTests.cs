using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class MinutesTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Minutes();

            Assert.Equal(5, result.TotalMinutes);
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
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfMinutes)
        {
            var result = numberOfMinutes.Minutes();

            Assert.Equal(numberOfMinutes, result.TotalMinutes);
        }
    }
}
