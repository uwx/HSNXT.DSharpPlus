using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeTrimTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Hello World", "Hello World")]
        [InlineData("\nHello World\n", "Hello World")]
        [InlineData("\tHello World\t", "Hello World")]
        [InlineData("  Hello World  ", "Hello World")]
        [InlineData("Hello   ", "Hello")]
        [InlineData("   World", "World")]
        [InlineData("Hello   World", "Hello   World")]
        public void InputReturnsExpectedOutput(string input, string expected)
        {
            var result = input.SafeTrim();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NullInputReturnsEmptyString()
        {
            string input = null;

            var result = input.SafeTrim();

            Assert.Equal(string.Empty, result);
        }
    }
}
