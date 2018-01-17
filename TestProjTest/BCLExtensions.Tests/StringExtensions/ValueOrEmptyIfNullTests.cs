using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueOrEmptyIfNullTests
    {
        [Fact]
        public void WithEmptyInputStringReturnsEmptyString()
        {
            var input = "";
            var result = input.ValueOrEmptyIfNull();
            Assert.Equal(string.Empty, result);
        }
        
        [Fact]
        public void WithNullInputStringReturnsEmptyString()
        {
            string input = null;
            var result = input.ValueOrEmptyIfNull();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WithNewLineInputStringReturnsOriginalString()
        {
            var input = "\n";
            var result = input.ValueOrEmptyIfNull();
            Assert.Equal(input, result);
        }

        [Fact]
        public void WithEmptySpacesInputStringReturnsOriginalString()
        {
            var input = "   ";
            var result = input.ValueOrEmptyIfNull();
            Assert.Equal(input, result);
        }

        [Fact]
        public void WithNonEmptyInputStringReturnsOriginalString()
        {
            var input = "The quick brown fox jumps over the lazy dog.";
            var result = input.ValueOrEmptyIfNull();
            Assert.Equal(input, result);
        }
    }
}
