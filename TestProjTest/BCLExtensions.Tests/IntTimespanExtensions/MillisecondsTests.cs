using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class MillisecondsTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Milliseconds();

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
            var result = numberOfMilliseconds.Milliseconds();

            Assert.Equal(numberOfMilliseconds, result.TotalMilliseconds);
        }
    }
}
