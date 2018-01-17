using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingWithTwoParametersTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42, "Test");
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int, string> action = SampleAction;

            var result = action.AsActionUsing(12, "12");

            Assert.NotNull(result);
        }

        [Fact]
        public void InternalActionExecutes()
        {
            var internalActionWasCalled = false;
            Action<int, string> action = (p1,p2) =>
            {
                internalActionWasCalled = true;
            };
            var result = action.AsActionUsing(12,"24");
            result();

            Assert.True(internalActionWasCalled);
        }

        [Fact]
        public void InternalActionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            var passedParameter1 = 0;
            string passedParameter2 = null;
            Action<int, string> action = (p1,p2) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
            };

            var result = action.AsActionUsing(expectedParameter1, expectedParameter2);
            result();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
        }

        private void SampleAction(int parameter1, string parameter2)
        {
        }

    }
}
