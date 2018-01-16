using Xunit; using HSNXT;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class IsAValidNumberOfDaysForTimeSpanTests
    {
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
        public void WhenNumberIsInRangeReturnsTrue(int numberOfDays)
        {
            var result = numberOfDays.IsAValidNumberOfDaysForTimeSpan();

            Assert.True(result);
        }

        [Theory]
        [InlineData(-10675200)]
        [InlineData(10675200)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void WhenNumberIsOutOfRangeReturnsFalse(int numberOfDays)
        {
            var result = numberOfDays.IsAValidNumberOfDaysForTimeSpan();

            Assert.False(result);
        }
    }
}
