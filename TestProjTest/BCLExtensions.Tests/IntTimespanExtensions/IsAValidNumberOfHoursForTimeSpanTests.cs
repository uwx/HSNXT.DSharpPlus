using Xunit; using HSNXT;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class IsAValidNumberOfHoursForTimeSpanTests
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
        [InlineData(-256204778)]
        [InlineData(256204778)]
        public void WhenNumberIsInRangeReturnsTrue(int numberOfDays)
        {
            var result = numberOfDays.IsAValidNumberOfHoursForTimeSpan();

            Assert.True(result);
        }

        [Theory]
        [InlineData(-256204779)]
        [InlineData(256204779)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void WhenNumberIsOutOfRangeReturnsFalse(int numberOfDays)
        {
            var result = numberOfDays.IsAValidNumberOfHoursForTimeSpan();

            Assert.False(result);
        }
    }
}
