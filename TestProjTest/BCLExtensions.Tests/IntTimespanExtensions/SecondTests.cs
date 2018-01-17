using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class SecondTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            var result = (5).Second();

            Assert.Equal(5, result.TotalSeconds);
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
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfSeconds)
        {
            var result = numberOfSeconds.Second();

            Assert.Equal(numberOfSeconds, result.TotalSeconds);
        }
    }
}
