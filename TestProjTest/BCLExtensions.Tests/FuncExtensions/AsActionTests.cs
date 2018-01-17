using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{

    public class AsActionTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction();
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int> function = SampleFunction;

            var action = function.AsAction();

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int> function = () =>
            {
                internalFunctionWasCalled = true;
                return 42;
            };
            var action = function.AsAction();
            action();

            Assert.True(internalFunctionWasCalled);
        }


        private int SampleFunction()
        {
            return 42;
        }

    }
}
