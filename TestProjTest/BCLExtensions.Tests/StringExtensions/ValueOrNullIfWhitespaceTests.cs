using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueOrNullIfWhitespaceTests
    {
        [Fact]
        public void WithEmptyInputStringReturnsNull()
        {
            var input = "";
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
            var input = "\n";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithEmptySpacesInputStringReturnsNull()
        {
            var input = "   ";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Null(result);
        }

        [Fact]
        public void WithNonEmptyInputStringReturnsOriginalString()
        {
            var input = "The quick brown fox jumps over the lazy dog.";
            var result = input.ValueOrNullIfWhitespace();
            Assert.Equal(input, result);
        }
    }
}
