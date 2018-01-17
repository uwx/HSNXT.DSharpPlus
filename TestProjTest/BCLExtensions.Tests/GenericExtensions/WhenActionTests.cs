using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.GenericExtensions
{
    public class WhenActionTests
    {

        [Fact]
        public void ValidateInputCanBeNull()
        {
            string input = null;
            
            Action<string> doNothing = DoNothing;
            var result = input.When(i => i != null, doNothing);

            Assert.Null(result);
        }

        [Fact]
        public void TruePredicateCallsAction()
        {
            var executed = TestActionExecution(AlwaysTrue);

            Assert.True(executed);
        }

        [Fact]
        public void TruePredicateReturnsInputValue()
        {
            var input = "Hello World";

            Action<string> action = DoNothing;
            var result = input.When(AlwaysTrue, action);

            Assert.Equal(input, result);
        }

        [Fact]
        public void FalsePredicateDoesNotCallFunction()
        {
            var executed = TestActionExecution(AlwaysFalse);

            Assert.False(executed);
        }

        [Fact]
        public void FalsePredicateReturnsInputValue()
        {
            var input = "Hello World";

            Action<string> action = DoNothing;
            var result = input.When(AlwaysFalse, action);

            Assert.Equal(input, result);
        }

        private static void DoNothing(string s)
        {
        }

        private bool TestActionExecution(Func<string, bool> predicate)
        {
            var executed = false;
            var input = "Hello World";

            input.When(predicate, s =>
            {
                executed = true;
            });
            return executed;
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
