using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class IsNullOrWhitespaceTests
    {
        [Fact]
        public void WhenInputHasContentThenReturnsFalse()
        {
            string input = "Test";
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
            string input = string.Empty;
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsEmptyStringThenReturnsTrue()
        {
            string input = "";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsWhitespaceThenReturnsTrue()
        {
            string input = "    ";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputIsNewlineThenReturnsTrue()
        {
            string input = "\n";
            var result = input.IsNullOrWhitespace();
            Assert.True(result);
        }
    }
}
