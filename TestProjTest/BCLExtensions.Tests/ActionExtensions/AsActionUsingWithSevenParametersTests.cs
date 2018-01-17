using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingWithSevenParametersTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42, "Test", true, 3.14m, byte.MaxValue, 98765, "Sample");
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int, string, bool, decimal, byte, int, string> action = SampleAction;

            var result = action.AsActionUsing(12, "12", false, 3.14m, byte.MaxValue, 666, "Class");

            Assert.NotNull(result);
        }

        [Fact]
        public void InternalActionExecutes()
        {
            var internalActionWasCalled = false;
            Action<int, string, bool, decimal, byte, int, string> action = (p1, p2, p3, p4, p5, p6, p7) =>
            {
                internalActionWasCalled = true;
            };
            var result = action.AsActionUsing(12, "24", false, 3.14m, byte.MaxValue, 12345, "Fish");
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
            const string expectedParameter7 = "24";
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            var passedParameter4 = 0.0m;
            byte passedParameter5 = 0;
            var passedParameter6 = 0;
            string passedParameter7 = null;
            Action<int, string, bool, decimal, byte, int, string> action = (p1, p2, p3, p4, p5, p6, p7) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                passedParameter4 = p4;
                passedParameter5 = p5;
                passedParameter6 = p6;
                passedParameter7 = p7;
            };

            var result = action.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4, expectedParameter5, expectedParameter6, expectedParameter7);
            result();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
            Assert.Equal(expectedParameter5, passedParameter5);
            Assert.Equal(expectedParameter6, passedParameter6);
            Assert.Equal(expectedParameter7, passedParameter7);
        }

        private void SampleAction(int parameter1, string parameter2, bool parameter3, decimal parameter4, byte parameter5, int parameter6, string parameter7)
        {
        }

    }
}
