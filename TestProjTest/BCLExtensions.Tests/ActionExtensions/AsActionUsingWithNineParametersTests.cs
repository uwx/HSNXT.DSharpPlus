using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingWithNineParametersTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42, "Test", true, 3.14m, byte.MaxValue, 98765, "Sample", false, 1.2345m);
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int, string, bool, decimal, byte, int, string, bool, decimal> action = SampleAction;

            var result = action.AsActionUsing(12, "12", false, 3.14m, byte.MaxValue, 666, "Class", true, 123.45m);

            Assert.NotNull(result);
        }

        [Fact]
        public void InternalActionExecutes()
        {
            var internalActionWasCalled = false;
            Action<int, string, bool, decimal, byte, int, string, bool, decimal> action = (p1, p2, p3, p4, p5, p6, p7, p8, p9) =>
            {
                internalActionWasCalled = true;
            };
            var result = action.AsActionUsing(12, "24", false, 3.14m, byte.MaxValue, 12345, "Fish", true, 1234.5m);
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
            const bool expectedParameter8 = false;
            const decimal expectedParameter9 = 12.5m;
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            var passedParameter4 = 0.0m;
            byte passedParameter5 = 0;
            var passedParameter6 = 0;
            string passedParameter7 = null;
            var passedParameter8 = true;
            var passedParameter9 = 0.0m;
            Action<int, string, bool, decimal, byte, int, string, bool, decimal> action = (p1, p2, p3, p4, p5, p6, p7, p8, p9) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                passedParameter4 = p4;
                passedParameter5 = p5;
                passedParameter6 = p6;
                passedParameter7 = p7;
                passedParameter8 = p8;
                passedParameter9 = p9;
            };

            var result = action.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4, expectedParameter5, expectedParameter6, expectedParameter7, expectedParameter8, expectedParameter9);
            result();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
            Assert.Equal(expectedParameter5, passedParameter5);
            Assert.Equal(expectedParameter6, passedParameter6);
            Assert.Equal(expectedParameter7, passedParameter7);
            Assert.Equal(expectedParameter8, passedParameter8);
            Assert.Equal(expectedParameter9, passedParameter9);
        }

        private void SampleAction(int parameter1, string parameter2, bool parameter3, decimal parameter4, byte parameter5, int parameter6, string parameter7, bool parameter8, decimal parameter9)
        {
        }

    }
}
