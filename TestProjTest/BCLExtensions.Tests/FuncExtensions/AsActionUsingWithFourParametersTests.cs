using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{

    public class AsActionUsingWithFourParametersTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42, "Test", true, 9.876m);
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, string, bool, decimal, decimal> function = SampleFunction;

            var action = function.AsActionUsing(12, "12", false, 5.4321m);

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int, string, bool, decimal, decimal> function = (p1,p2,p3,p4) =>
            {
                internalFunctionWasCalled = true;
                return 42m;
            };
            var action = function.AsActionUsing(12,"24",true, 3.14m);
            action();

            Assert.True(internalFunctionWasCalled);
        }


        [Fact]
        public void InternalFunctionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            const bool expectedParameter3 = true;
            const decimal expectedParameter4 = 3.14m;
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            var passedParameter4 = 0.0m;
            Func<int, string, bool, decimal, decimal> function = (p1,p2,p3,p4) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                passedParameter4 = p4;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4);
            action();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
        }

        private decimal SampleFunction(int parameter1, string parameter2, bool parameter3, decimal parameter4)
        {
            return 42m;
        }

    }
}
