using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class IsNullOrWhitespaceTests
    {
        [Fact]
        public void WhenInputHasContentThenReturnsFalse()
        {
            var input = "Test";
            var result = input.IsNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputNullThenReturnsTrue()
        {
            string input = null;
            var result= input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsStringEmptyThenReturnsTrue()
        {
            var input = string.Empty;
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsEmptyStringThenReturnsTrue()
        {
            var input = "";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsWhitespaceThenReturnsTrue()
        {
            var input = "    ";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsNewlineThenReturnsTrue()
        {
            var input = "\n";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }
    }
}
