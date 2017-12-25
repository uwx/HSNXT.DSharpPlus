﻿using System; using HSNXT;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class DayTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            TimeSpan result = (1).Day();

            Assert.Equal(1, result.TotalDays);
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
        [InlineData(-10675199)]
        [InlineData(10675199)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfDays)
        {
            TimeSpan result = numberOfDays.Day();

            Assert.Equal(numberOfDays, result.TotalDays);
        }
    }
}
