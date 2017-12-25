using System; using HSNXT;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueorIfNullTests
    {
        public class WithNullInputString
        {
            private readonly string _input = null;

            [Fact]
            public void DefaultStringReturnsDefaultString()
            {
                string expected = "(Default)";
                var result = _input.ValueOrIfNull(expected);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void EmptyDefaultStringReturnsEmptyString()
            {
                var result = _input.ValueOrIfNull(string.Empty);
                Assert.Equal(string.Empty, result);
            }
        }

        [Fact]
        public void WithEmptyStringReturnsEmptyString()
        {
            string input = "";
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithStringEmptyReturnsEmptyString()
        {
            string input = string.Empty;
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithWhitespaceReturnsInputString()
        {
            string input = "   ";
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(input, result);
        }
    }
}
