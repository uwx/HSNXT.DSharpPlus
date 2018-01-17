using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingWithSixParametersTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42, "Test", true, 3.14m, byte.MaxValue, 98765);
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int, string, bool, decimal, byte, int> action = SampleAction;

            var result = action.AsActionUsing(12, "12", false, 3.14m, byte.MaxValue, 666);

            Assert.NotNull(result);
        }

        [Fact]
        public void InternalActionExecutes()
        {
            var internalActionWasCalled = false;
            Action<int, string, bool, decimal, byte, int> action = (p1, p2, p3, p4, p5, p6) =>
            {
                internalActionWasCalled = true;
            };
            var result = action.AsActionUsing(12, "24", false, 3.14m, byte.MaxValue, 12345);
            result();

            Assert.True(internalActionWasCalled);
        }

        [Fact]
        public void InternalActionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            const bool expectedParameter3 = true;
            const decimal expectedParameter4 = 4.2m;
            const byte expectedParameter5 = 127;
            const int expectedParameter6 = 67;
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            var passedParameter4 = 0.0m;
            byte passedParameter5 = 0;
            var passedParameter6 = 0;
            Action<int, string, bool, decimal, byte, int> action = (p1, p2, p3, p4, p5, p6) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                passedParameter4 = p4;
                passedParameter5 = p5;
                passedParameter6 = p6;
            };

            var result = action.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4, expectedParameter5, expectedParameter6);
            result();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
            Assert.Equal(expectedParameter5, passedParameter5);
            Assert.Equal(expectedParameter6, passedParameter6);
        }

        private void SampleAction(int parameter1, string parameter2, bool parameter3, decimal parameter4, byte parameter5, int parameter6)
        {
        }

    }
}
