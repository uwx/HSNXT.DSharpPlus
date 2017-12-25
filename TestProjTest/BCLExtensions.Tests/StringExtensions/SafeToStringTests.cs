using Xunit; using HSNXT;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeToStringTests
    {
        [Fact]
        public void NullReturnsEmptyString()
        {
            object value = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = value.SafeToString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void StringReturnsOriginalString()
        {
            const string expected = "String Object";

            var result = expected.SafeToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void StringAsObjectReturnsOriginalString()
        {
            const string expected = "Test String";
            object value = expected;

            var result = value.SafeToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CustomObjectReturnsCorrectString()
        {
            var value = new TestObject();

            var result = value.SafeToString();

            Assert.Equal(TestObject.ExpectedString, result);
        }

        private class TestObject
        {
            public const string ExpectedString = "Sample String";

            public override string ToString()
            {
                return ExpectedString;
            }
        }
    }
}
