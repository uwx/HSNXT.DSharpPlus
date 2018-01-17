using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.TimeSpanExtensions
{
    public class AgoTests
    {
        [Fact]
        public void WorksWhenUsedAsAnExtension()
        {
            var duration = (2).Minutes();
            var result = duration.Ago();
            var now = DateTime.Now;

            var errorMargin = 2.Seconds();
            var resultBelowUpperBound = result <= (now - duration + errorMargin);
            var resultAboveLowerBound = result >= (now - duration) - errorMargin;
            Assert.True(resultBelowUpperBound && resultAboveLowerBound);
        }

    }
}
