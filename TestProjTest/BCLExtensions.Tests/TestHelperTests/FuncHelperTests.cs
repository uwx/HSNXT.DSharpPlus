using BCLExtensions.Tests.TestHelpers;
using Xunit;

namespace BCLExtensions.Tests.TestHelperTests
{
    public class FuncHelperTests
    {
        [Fact]
        public void FourtyTwoReturnsFalse()
        {
            var result = FuncHelpers.ReturnFalse(42);

            Assert.False(result);
        }

        [Fact]
        public void NullStringReturnsFalse()
        {
            var result = FuncHelpers.ReturnFalse<string>(null);

            Assert.False(result);
        }

        [Fact]
        public void StringReturnsFalse()
        {
            var result = FuncHelpers.ReturnFalse("Hello World");

            Assert.False(result);
        }

        [Fact]
        public void SelectSelfWorksWithString()
        {
            var result = FuncHelpers.SelectSelf("Hello World");

            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void SelectSelfWorksWithNullString()
        {
            var result = FuncHelpers.SelectSelf<string>(null);

            Assert.Equal(null, result);
        }

        [Fact]
        public void SelectSelfWorksWithInt()
        {
            var result = FuncHelpers.SelectSelf(42);

            Assert.Equal(42, result);
        }
    }
}
