using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeRightTests
    {
        [Theory]
        [InlineData("Hello World", 5, "World")]
        [InlineData("Not Long Enough", 100, "Not Long Enough")]
        [InlineData("", 0, "")]
        [InlineData("", 5, "")]
        [InlineData("Becomes Empty String", 0, "")]
        public void InputReturnsExpectedOutput(string input, int length, string expected)
        {
            var formattedString = input.SafeRight(length);
            Assert.Equal(expected, formattedString);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        public void NullInputReturnsEmptyString(int length)
        {
            string input = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var formattedString = input.SafeRight(length);
            
            Assert.Equal(string.Empty, formattedString);
        }
    }
}
