using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.TimeSpanExtensions
{
    public class BeforeTests
    {
        [Fact]
        public void WorksWhenUsedAsAnExtension()
        {
            var now = DateTime.Now;
            var result = 5.Minutes().Before(now);
            Assert.Equal(now - TimeSpan.FromMinutes(5), result);
        }
    }
}
