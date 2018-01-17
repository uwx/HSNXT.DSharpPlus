using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class HoursTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Hours();

            Assert.Equal(5, result.TotalHours);
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
        [InlineData(-256204778)]
        [InlineData(256204778)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfHours)
        {
            var result = numberOfHours.Hours();

            Assert.Equal(numberOfHours, result.TotalHours);
        }
    }
}
