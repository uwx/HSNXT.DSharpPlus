using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.GenericExtensions
{
    public class WhenFunctionTests
    {
        private string _newValue = "New World";

        [Fact]
        public void ValidateInputCanBeNull()
        {
            string input = null;
            
            Func<string,string> returnsNewValue = ReturnsNewValue;
            var result = input.When(i => i != null, returnsNewValue);

            Assert.Null(result);
        }

        [Fact]
        public void TruePredicateCallsFunction()
        {
            var executed = TestFunctionExecution(AlwaysTrue);

            Assert.True(executed);
        }

        [Fact]
        public void TruePredicateReturnsFunctionResult()
        {
            var input = "Hello World";

            Func<string, string> function = ReturnsNewValue;
            var result = input.When(AlwaysTrue, function);

            Assert.Equal(_newValue, result);
        }

        [Fact]
        public void FalsePredicateDoesNotCallFunction()
        {
            var executed = TestFunctionExecution(AlwaysFalse);

            Assert.False(executed);
        }

        [Fact]
        public void FalsePredicateReturnsInputValue()
        {
            var input = "Hello World";

            Func<string,string> function = ReturnsNewValue;
            var result = input.When(AlwaysFalse, function);

            Assert.Equal(input, result);
        }
        [Fact]
        public void GivenANullStringCanConditionalReplaceString()
        {
            string input = null;
            var newString = "New String";

            var result = input.When(i => i == null, i => newString);

            Assert.Equal(newString, result);
        }

        [Fact]
        public void GivenALongStringCanConditionalSubString()
        {
            var value = "Hello";

            var result = CallLengthWhenOnString(value);

            Assert.Equal("Hel", result);
        }

        [Fact]
        public void GivenAShortStringOriginalStringReturns()
        {
            var value = "He";

            var result = CallLengthWhenOnString(value);

            Assert.Equal("He", result);
        }

        private static string CallLengthWhenOnString(string value)
        {
            var result = value.When(v => v.Length > 3, v => v.Substring(0, 3));
            return result;
        }
		
        private bool TestFunctionExecution(Func<string, bool> predicate)
        {
            var executed = false;
            var input = "Hello World";

            input.When(predicate, s =>
            {
                executed = true;
                return s;
            });
            return executed;
        }

        private string ReturnsNewValue(string s)
        {
            return _newValue;
        }

        private bool AlwaysFalse(string s)
        {
            return false;
        }

        private bool AlwaysTrue(string s)
        {
            return true;
        }

    }
}
