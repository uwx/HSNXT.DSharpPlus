﻿using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntExtensions
{
    public class IsBetweenExclusiveTests
    {
        [Theory]
        [InlineData(0,-1,1)]
        [InlineData(0,int.MinValue,int.MaxValue)]
        [InlineData(42, 30, 60)]
        [InlineData(-30, -40, -20)]
        public void InputToIsBetweenWithLimitsReturnsTrue(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetweenExclusive(lowerLimit, upperLimit);

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(42, 42, 42)]
        [InlineData(5, 5, 6)]
        [InlineData(6, 5, 6)]
        [InlineData(0, 1, 2)]
        [InlineData(0, -2, -1)]
        [InlineData(42, 5, 12)]
        [InlineData(42, 50, 100)]
        [InlineData(-30, -20, -10)]
        [InlineData(-30, -50, -40)]
        public void InputToIsBetweenWithLimitsReturnsFalse(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetweenExclusive(lowerLimit, upperLimit);

            Assert.False(result);
        }
    }
}
