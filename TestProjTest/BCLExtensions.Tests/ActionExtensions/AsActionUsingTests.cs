using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42);
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int> function = SampleAction;

            var action = function.AsActionUsing(12);

            Assert.NotNull(action);
        }

        private void SampleAction(int parameter)
        {
            
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Action<int> action = parameter =>
            {
                internalFunctionWasCalled = true;
            };
            var result = action.AsActionUsing(12);
            result();

            Assert.True(internalFunctionWasCalled);
        }


        [Fact]
        public void InternalFunctionCapturesCorrectParameter()
        {
            const int expectedParameter = 12;
            var passedParameter = 0;
            Action<int> action = parameter =>
            {
                passedParameter = parameter;
            };

            var result = action.AsActionUsing(expectedParameter);
            result();

            Assert.Equal(expectedParameter, passedParameter);
        }
    }
}
