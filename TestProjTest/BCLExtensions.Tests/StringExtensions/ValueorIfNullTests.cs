using HSNXT;
using Xunit;

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
                var expected = "(Default)";
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
            var input = "";
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithStringEmptyReturnsEmptyString()
        {
            var input = string.Empty;
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithWhitespaceReturnsInputString()
        {
            var input = "   ";
            var result = input.ValueOrIfNull("(Default)");
            Assert.Equal(input, result);
        }
    }
}
