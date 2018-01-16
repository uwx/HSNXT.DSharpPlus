using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeTrimEndTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Hello World", "Hello World")]
        [InlineData("\nHello World\n", "\nHello World")]
        [InlineData("\tHello World\t", "\tHello World")]
        [InlineData("  Hello World  ", "  Hello World")]
        [InlineData("Hello   ", "Hello")]
        [InlineData("   World", "   World")]
        [InlineData("Hello   World", "Hello   World")]
        public void InputReturnsExpectedOutput(string input, string expected)
        {
            var result = input.SafeTrimEnd();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NullInputReturnsEmptyString()
        {
            string input = null;

            var result = input.SafeTrimEnd();

            Assert.Equal(string.Empty, result);
        }
    }
}
