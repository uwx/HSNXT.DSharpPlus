using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueOrNullIfWhitespaceTests
    {
        [Fact]
        public void WithEmptyInputStringReturnsNull()
        {
            string input = "";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithNullInputStringReturnsNull()
        {
            string input = null;
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithNewLineInputStringReturnsNull()
        {
            string input = "\n";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithEmptySpacesInputStringReturnsNull()
        {
            string input = "   ";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithNonEmptyInputStringReturnsOriginalString()
        {
            string input = "The quick brown fox jumps over the lazy dog.";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Equal(input, result);
        }
    }
}
