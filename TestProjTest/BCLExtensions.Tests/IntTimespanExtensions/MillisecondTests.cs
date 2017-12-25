using System; using HSNXT;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class MillisecondTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            TimeSpan result = (5).Millisecond();

            Assert.Equal(5, result.TotalMilliseconds);
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
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfMilliseconds)
        {
            TimeSpan result = numberOfMilliseconds.Millisecond();

            Assert.Equal(numberOfMilliseconds, result.TotalMilliseconds);
        }
    }
}
