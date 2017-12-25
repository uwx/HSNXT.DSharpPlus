using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.GenericExtensions
{
    public class TeeTests
    {
        [Fact]
        public void TeeReturnsProvidedInput()
        {
            var value = "Hello There";

            var result = value.Tee(t => { });

            Assert.Equal(value, result);
        }

        [Fact]
        public void TeeActionIsCalled()
        {
            var value = "Hello There";
            var called = false;

            value.Tee(t => called = true);

            Assert.True(called);
        }
    }
}
