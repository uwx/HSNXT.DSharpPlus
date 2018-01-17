using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{
    public class AsActionUsingTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42);
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, int> function = SampleFunction;

            var action = function.AsActionUsing(12);

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int, int> function = parameter =>
            {
                internalFunctionWasCalled = true;
                return 42;
            };
            var action = function.AsActionUsing(12);
            action();

            Assert.True(internalFunctionWasCalled);
        }

        [Fact]
        public void InternalFunctionCapturesCorrectParameter()
        {
            const int expectedParameter = 12;
            var passedParameter = 0;
            Func<int, int> function = parameter =>
            {
                passedParameter = parameter;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter);
            action();

            Assert.Equal(expectedParameter, passedParameter);
        }

        private int SampleFunction(int parameter)
        {
            return parameter;
        }
    }
}
