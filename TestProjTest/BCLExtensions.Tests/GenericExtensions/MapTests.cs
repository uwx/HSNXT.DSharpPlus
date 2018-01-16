using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.GenericExtensions
{
    public class MapTests
    {
        [Fact]
        public void MapExecutesWhenRun()
        {
            const string value = "Hello World";

            var result = value.Map(x => x.Length);

            Assert.Equal(11, result);
        }

        [Fact]
        public void MapCanPassThroughSelf()
        {
            const string value = "Hello World";

            var result = value.Map(x => x);

            Assert.Equal(value, result);
        }
    }
}
