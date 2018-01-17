using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{
    public class AsActionUsingWithTwoParametersTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42, "Test");
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, string, decimal> function = SampleFunction;

            var action = function.AsActionUsing(12, "12");

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int, string, decimal> function = (p1,p2) =>
            {
                internalFunctionWasCalled = true;
                return 42m;
            };
            var action = function.AsActionUsing(12,"24");
            action();

            Assert.True(internalFunctionWasCalled);
        }

        [Fact]
        public void InternalFunctionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            var passedParameter1 = 0;
            string passedParameter2 = null;
            Func<int, string, decimal> function = (p1,p2) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter1, expectedParameter2);
            action();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
        }

        private decimal SampleFunction(int parameter1, string parameter2)
        {
            return 42m;
        }
    }
}
