using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class IsNotNullOrWhitespaceTests
    {
        [Fact]
        public void WhenInputHasContentThenReturnsTrue()
        {
            var input = "Test";
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
            var input = string.Empty;
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsEmptyStringThenReturnsFalse()
        {
            var input = "";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsWhitespaceThenReturnsFalse()
        {
            var input = "    ";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }

        [Fact]
        public void WhenInputIsNewlineThenReturnsFalse()
        {
            var input = "\n";
            var result = input.IsNotNullOrWhitespace();
            Assert.False(result);
        }
    }
}
