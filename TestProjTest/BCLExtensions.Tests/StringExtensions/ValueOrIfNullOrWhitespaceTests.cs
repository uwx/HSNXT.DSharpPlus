using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.StringExtensions
{
    public class ValueOrIfNullOrWhitespaceTests
    {
        public abstract class WithInputStringBase
        {
            protected abstract string input{ get; }

            [Fact]
            public void DefaultStringReturnsDefaultString()
            {
                var expected = "(Default)";
                var result = input.ValueOrIfNullOrWhitespace(expected);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void EmptyDefaultStringReturnsEmptyString()
            {
                var result = input.ValueOrIfNullOrWhitespace(string.Empty);
                Assert.Equal(string.Empty, result);
            }
        }

        public class WithNullInputString : WithInputStringBase
        {
            protected override string input
            {
                get { return null; }
            }
        }

        public class WithEmptyInputString : WithInputStringBase
        {
            protected override string input
            {
                get { return string.Empty; }
            }
        }

        public class WithWhitespaceInputString : WithInputStringBase
        {
            protected override string input
            {
                get { return "    "; }
            }
        }

        public class WithNewlineInputString : WithInputStringBase
        {
            protected override string input
            {
                get { return "\n"; }
            }
        }

        public class WithNonNullNonWhitespaceInputString
        {
            private readonly string input = "Test";

            [Fact]
            public void WithStringEmptyReplacementReturnsInputString()
            {
                var result = input.ValueOrIfNullOrWhitespace(string.Empty);
                Assert.Equal(input, result);
            }
            [Fact]
            public void WithNonNullNonWhitespaceReplacementReturnsInputString()
            {
                var result = input.ValueOrIfNullOrWhitespace("(Default)");
                Assert.Equal(input, result);
            }
        }
    }
}
