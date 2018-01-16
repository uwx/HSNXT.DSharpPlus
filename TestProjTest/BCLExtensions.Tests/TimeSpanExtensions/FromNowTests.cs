using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.TimeSpanExtensions
{
    public class FromNowTests
    {
        [Fact]
        public void WorksWhenUsedAsAnExtension()
        {
            TimeSpan duration = 2.Minutes();
            DateTime result = duration.FromNow();
            var now = DateTime.Now;

            var errorMargin = 1.Second();
            var resultBelowUpperBound = result <= (now + duration + errorMargin);
            var resultAboveLowerBound = result >= (now + duration) - errorMargin;
            Assert.True(resultBelowUpperBound && resultAboveLowerBound);
        }
    }
}
