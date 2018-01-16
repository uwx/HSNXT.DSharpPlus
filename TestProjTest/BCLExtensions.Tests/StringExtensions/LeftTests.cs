using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.StringExtensions
{
    public class LeftTests
    {
        [Theory]
        [InlineData("Hello World", 5, "Hello")]
        [InlineData("", 0, "")]
        [InlineData("Becomes Empty String", 0, "")]
        public void InputReturnsExpectedOutput(string input, int length, string expected)
        {
            var formattedString = input.Left(length);
            Assert.Equal(expected, formattedString);
        }
    }
}
