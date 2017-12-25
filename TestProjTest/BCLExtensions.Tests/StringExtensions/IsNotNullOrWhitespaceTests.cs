using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class IsNotNullOrWhitespaceTests
    {
        [Fact]
        public void WhenInputHasContentThenReturnsTrue()
        {
            string input = "Test";
            var result = input.IsNotNullOrWhitespace();
            Assert.True(result);
        }

        [Fact]
        public void WhenInputNullThenReturnsFalse()
        {
            string input = null;
            var result= input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsStringEmptyThenReturnsFalse()
        {
            string input = string.Empty;
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsEmptyStringThenReturnsFalse()
        {
            string input = "";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsWhitespaceThenReturnsFalse()
        {
            string input = "    ";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsNewlineThenReturnsFalse()
        {
            string input = "\n";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }
    }
}
