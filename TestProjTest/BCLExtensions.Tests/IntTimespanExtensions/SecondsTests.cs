using System; using HSNXT;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class SecondsTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            TimeSpan result = (5).Seconds();

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
            TimeSpan result = numberOfSeconds.Seconds();

            Assert.Equal(numberOfSeconds, result.TotalSeconds);
        }
    }
}
