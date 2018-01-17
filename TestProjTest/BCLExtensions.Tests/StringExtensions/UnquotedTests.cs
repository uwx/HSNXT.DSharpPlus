using Xunit; using HSNXT;
// ReSharper disable ExpressionIsAlwaysNull

namespace BCLExtensions.Tests.StringExtensions
{
    public class UnquotedTests
    {
        [Fact]
        public void NullReturnsNull()
        {
            string input=null;

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }
        
        [Fact]
        public void EmptyStringReturnsEmpty()
        {
            var input = string.Empty;

            var output = input.Unquoted();

            Assert.Equal(string.Empty, output);
        }

        [Fact]
        public void UnquotedStringReturnsOriginalString()
        {
            const string input = "This string has no quotes";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringContainingOneQuoteReturnsOriginalString()
        {
            const string input = "This string has no \" characters surrounding it";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithEmbeddedQuotesReturnsOriginalString()
        {
            const string input = "This string has no \"quotes\" surrounding it";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithQuotesReturnsWithoutQuotes()
        {
            const string input = "\"This string has quotes surrounding it\"";

            var output = input.Unquoted();

            Assert.Equal("This string has quotes surrounding it", output);
        }

        [Fact]
        public void StringWithTwoSetsOfQuotesReturnsWithoutOneSetOfQuotes()
        {
            const string input = "\"\"This string has quotes surrounding it\"\"";

            var output = input.Unquoted();

            Assert.Equal("\"This string has quotes surrounding it\"", output);
        }

        [Fact]
        public void StringWithSurroundingAndEmbeddedQuotesReturnsWithoutQuotesWithEmbedded()
        {
            const string input = "\"This string has no \"quotes\" surrounding it\"";

            var output = input.Unquoted();

            Assert.Equal("This string has no \"quotes\" surrounding it", output);
        }

        [Fact]
        public void StringContainingSurroundingQuotesAndOneQuoteReturnsWithoutSurroundingQuotes()
        {
            const string input = "\"This string has no \" characters surrounding it\"";

            var output = input.Unquoted();

            Assert.Equal("This string has no \" characters surrounding it", output);
        }

        [Fact]
        public void QuotesOnlyStringBecomesEmptyString()
        {
            const string input = "\"\"";

            var output = input.Unquoted();

            Assert.Equal(string.Empty, output);
        }

        [Fact]
        public void QuoteCharacterStringReturnsOriginalString()
        {
            const string input = "\"";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringContainingOneSingleQuoteReturnsOriginalString()
        {
            const string input = "This string has no \' characters surrounding it";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithEmbeddedSingleQuotesReturnsOriginalString()
        {
            const string input = "This string has no \'quotes\' surrounding it";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithSingleQuotesReturnsWithoutSingleQuotes()
        {
            const string input = "\'This string has quotes surrounding it\'";

            var output = input.Unquoted();

            Assert.Equal("This string has quotes surrounding it", output);
        }

        [Fact]
        public void StringWithTwoSetsOfSingleQuotesReturnsWithoutOneSetOfSingleQuotes()
        {
            const string input = "\'\'This string has quotes surrounding it\'\'";

            var output = input.Unquoted();

            Assert.Equal("\'This string has quotes surrounding it\'", output);
        }

        [Fact]
        public void StringWithSurroundingAndEmbeddedSingleQuotesReturnsWithoutSingleQuotesWithEmbedded()
        {
            const string input = "\'This string has no \'quotes\' surrounding it\'";

            var output = input.Unquoted();

            Assert.Equal("This string has no \'quotes\' surrounding it", output);
        }

        [Fact]
        public void StringContainingSurroundingSingleQuotesAndSingleQuoteReturnsWithoutSurroundingSingleQuotes()
        {
            const string input = "\'This string has no \' characters surrounding it\'";

            var output = input.Unquoted();

            Assert.Equal("This string has no \' characters surrounding it", output);
        }

        [Fact]
        public void SingleQuotesOnlyStringBecomesEmptyString()
        {
            const string input = "\'\'";

            var output = input.Unquoted();

            Assert.Equal(string.Empty, output);
        }

        [Fact]
        public void SingleQuoteCharacterStringReturnsOriginalString()
        {
            const string input = "\'";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithSingleWrappingDoubleQuotesReturnsWithoutOneSetOfDoubleQuotes()
        {
            const string input = "\'\"This string has quotes surrounding it\"\'";

            var output = input.Unquoted();

            Assert.Equal("\"This string has quotes surrounding it\"", output);
        }

        [Fact]
        public void StringWithDoubleWrappingSingleQuotesReturnsWithoutOneSetOfSingleQuotes()
        {
            const string input = "\"\'This string has quotes surrounding it\'\"";

            var output = input.Unquoted();

            Assert.Equal("\'This string has quotes surrounding it\'", output);
        }

        [Fact]
        public void StringWithSurroundingSingleQuotesAndEmbeddedQuotesReturnsWithoutSingleQuotesWithEmbedded()
        {
            const string input = "\'This string has no \"quotes\" surrounding it\'";

            var output = input.Unquoted();

            Assert.Equal("This string has no \"quotes\" surrounding it", output);
        }

        [Fact]
        public void StringWithSurroundingQuotesAndEmbeddedSingleQuotesReturnsWithoutQuotesWithEmbedded()
        {
            const string input = "\"This string has no \'quotes\' surrounding it\"";

            var output = input.Unquoted();

            Assert.Equal("This string has no \'quotes\' surrounding it", output);
        }

        [Fact]
        public void StringContainingSurroundingSingleQuotesAndDoubleQuoteReturnsWithoutSurroundingSingleQuotes()
        {
            const string input = "\'This string has no \" characters surrounding it\'";

            var output = input.Unquoted();

            Assert.Equal("This string has no \" characters surrounding it", output);
        }

        [Fact]
        public void StringContainingSurroundingQuotesAndOneSingleQuoteReturnsWithoutSurroundingQuotes()
        {
            const string input = "\"This string has no \' characters surrounding it\"";

            var output = input.Unquoted();

            Assert.Equal("This string has no \' characters surrounding it", output);
        }

        [Fact]
        public void StringWithSingleAndDoubleQuotesTransposedReturnsOriginalString()
        {
            const string input = "\'\"This string has quotes surrounding it\'\"";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }

        [Fact]
        public void StringWithSingleAndDoubleQuotesTransposedTheOtherWayReturnsOriginalString()
        {
            const string input = "\"\'This string has quotes surrounding it\"\'";

            var output = input.Unquoted();

            Assert.Equal(input, output);
        }
    }
}
