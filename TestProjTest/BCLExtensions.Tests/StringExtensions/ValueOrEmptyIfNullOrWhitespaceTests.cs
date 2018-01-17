using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueOrEmptyIfNullOrWhitespaceTests
    {
        [Fact]
        public void WithEmptyInputStringReturnsEmptyString()
        {
            var input = "";
            var result = input.ValueOrEmptyIfNullOrWhitespace();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithNullInputStringReturnsEmptyString()
        {
            string input = null;
            var result = input.ValueOrEmptyIfNullOrWhitespace();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithNewLineInputStringReturnsEmptyString()
        {
            var input = "\n";
            var result = input.ValueOrEmptyIfNullOrWhitespace();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithEmptySpacesInputStringReturnsEmptyString()
        {
            var input = "   ";
            var result = input.ValueOrEmptyIfNullOrWhitespace();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithNonEmptyInputStringReturnsOriginalString()
        {
            var input = "The quick brown fox jumps over the lazy dog.";
            var result = input.ValueOrEmptyIfNullOrWhitespace();
            Assert.Equal(input, result);
        }
    }
}
