using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.StringExtensions
{
    public class RightTests
    {
        [Theory]
        [InlineData("Hello World", 5, "World")]
        [InlineData("", 0, "")]
        [InlineData("Becomes Empty String", 0, "")]
        public void InputReturnsExpectedOutput(string input, int length, string expected)
        {
            var formattedString = input.Right(length);
            Assert.Equal(expected, formattedString);
        }
    }
}
