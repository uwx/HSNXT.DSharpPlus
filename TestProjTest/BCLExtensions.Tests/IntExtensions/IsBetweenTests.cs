using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.IntExtensions
{
    public class IsBetweenTests
    {
        [Theory]
        [InlineData(0,-1,1)]
        [InlineData(0,int.MinValue,int.MaxValue)]
        [InlineData(42, 30, 60)]
        [InlineData(-30, -40, -20)]
        public void InputToIsBetweenWithLimitsReturnsTrue(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetween(lowerLimit, upperLimit);

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(0, -2, -1)]
        [InlineData(42, 5, 12)]
        [InlineData(42, 50, 100)]
        [InlineData(-30, -20, -10)]
        [InlineData(-30, -50, -40)]
        public void InputToIsBetweenWithLimitsReturnsFalse(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetween(lowerLimit, upperLimit);

            Assert.False(result);
        }
    }
}
