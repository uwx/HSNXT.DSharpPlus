﻿using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.TimeSpanExtensions
{
    public class AfterTests
    {
        [Fact]
        public void WorksWhenUsedAsAnExtension()
        {
            var now = DateTime.Now;
            var result = 5.Minutes().After(now);
            Assert.Equal(now + TimeSpan.FromMinutes(5), result);
        }
    }
}
