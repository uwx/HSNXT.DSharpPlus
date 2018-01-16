using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeTrimStartTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Hello World", "Hello World")]
        [InlineData("\nHello World\n", "Hello World\n")]
        [InlineData("\tHello World\t", "Hello World\t")]
        [InlineData("  Hello World  ", "Hello World  ")]
        [InlineData("Hello   ", "Hello   ")]
        [InlineData("   World", "World")]
        [InlineData("Hello   World", "Hello   World")]
        public void InputReturnsExpectedOutput(string input, string expected)
        {
            var result = input.SafeTrimStart();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NullInputReturnsEmptyString()
        {
            string input = null;

            var result = input.SafeTrimStart();

            Assert.Equal(string.Empty, result);
        }
    }
}
