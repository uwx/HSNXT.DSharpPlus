using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.StringExtensions
{
    public class SafeToStringWithNullStringTests
    {
        [Fact]
        public void NullReturnsEmptyString()
        {
            object value = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = value.SafeToString("[NULL]");

            Assert.Equal("[NULL]", result);
        }

        [Fact]
        public void StringReturnsOriginalString()
        {
            const string expected = "String Object";

            var result = expected.SafeToString("[NULL]");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void StringAsObjectReturnsOriginalString()
        {
            const string expected = "Test String";
            object value = expected;

            var result = value.SafeToString("[NULL]");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CustomObjectReturnsCorrectString()
        {
            var value = new TestObject();

            var result = value.SafeToString("[NULL]");

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
