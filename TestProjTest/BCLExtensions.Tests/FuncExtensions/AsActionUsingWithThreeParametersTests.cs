using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{
    public class AsActionUsingWithThreeParametersTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42, "Test", true);
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, string, bool, decimal> function = SampleFunction;

            var action = function.AsActionUsing(12, "12", false);

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int, string, bool, decimal> function = (p1,p2,p3) =>
            {
                internalFunctionWasCalled = true;
                return 42m;
            };
            var action = function.AsActionUsing(12,"24",true);
            action();

            Assert.True(internalFunctionWasCalled);
        }

        [Fact]
        public void InternalFunctionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            const bool expectedParameter3 = true;
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            Func<int, string, bool, decimal> function = (p1,p2,p3) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3);
            action();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
        }

        private decimal SampleFunction(int parameter1, string parameter2, bool parameter3)
        {
            return 42m;
        }
    }
}
