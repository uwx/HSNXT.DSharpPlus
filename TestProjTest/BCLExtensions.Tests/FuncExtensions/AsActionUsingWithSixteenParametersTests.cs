
using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{
    public class AsActionUsingWithSixteenParametersTests
    {
        private const byte ByteValue = byte.MaxValue - 128;

        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42, "Test", true, 9.876m, byte.MaxValue, 12, "Foo", false, 3.33333m, ByteValue, 9000, "Test", true, 987.654m, ByteValue, new object());
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, string, bool, decimal, byte, int, string, bool, decimal, byte, int?, string, bool?, decimal?, byte?, object, decimal> function = SampleFunction;

            var action = function.AsActionUsing(12, "12", false, 5.4321m, byte.MaxValue, 42, "Bar", true, 66666.3m, ByteValue, 1600, "Class", false, 123.456m, ByteValue, new object());

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            var internalFunctionWasCalled = false;
            Func<int, string, bool, decimal, byte, int, string, bool, decimal, byte, int?, string, bool?, decimal?, byte?, object, decimal> function = (p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16) =>
            {
                internalFunctionWasCalled = true;
                return 42m;
            };
            var action = function.AsActionUsing(12,"24",true, 3.14m, byte.MaxValue, 16, "Fizz", false, 452.6m, ByteValue, 65536, "Malloc", true, 987.123m, ByteValue, new object());
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
            const byte expectedParameter5 = 127;
            const int expectedParameter6 = 63;
            const string expectedParameter7 = "Buzz";
            const bool expectedParameter8 = false;
            const decimal expectedParameter9 = 863.732m;
            const byte expectedParameter10 = 63;
            int? expectedParameter11 = 170;
            const string expectedParameter12 = "Batch";
            bool? expectedParameter13 = true;
            decimal? expectedParameter14 = 1234.567m;
            byte? expectedParameter15 = 31;
            var expectedParameter16 = new object();
            var passedParameter1 = 0;
            string passedParameter2 = null;
            var passedParameter3 = false;
            var passedParameter4 = 0.0m;
            byte passedParameter5 = 0;
            var passedParameter6 = 0;
            string passedParameter7 = null;
            var passedParameter8 = false;
            var passedParameter9 = 0m;
            byte passedParameter10 = 0;
            int? passedParameter11 = null;
            string passedParameter12 = null;
            bool? passedParameter13 = false;
            decimal? passedParameter14 = null;
            byte? passedParameter15 = null;
            object passedParameter16 = null;
            Func<int, string, bool, decimal, byte, int, string, bool, decimal, byte, int?, string, bool?, decimal?, byte?, object, decimal> function = (p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16) =>
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
                passedParameter10 = p10;
                passedParameter11 = p11;
                passedParameter12 = p12;
                passedParameter13 = p13;
                passedParameter14 = p14;
                passedParameter15 = p15;
                passedParameter16 = p16;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4, expectedParameter5, expectedParameter6, expectedParameter7, expectedParameter8, expectedParameter9, expectedParameter10, expectedParameter11, expectedParameter12, expectedParameter13, expectedParameter14, expectedParameter15, expectedParameter16);
            action();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
            Assert.Equal(expectedParameter5, passedParameter5);
            Assert.Equal(expectedParameter6, passedParameter6);
            Assert.Equal(expectedParameter7, passedParameter7);
            Assert.Equal(expectedParameter8, passedParameter8);
            Assert.Equal(expectedParameter9, passedParameter9);
            Assert.Equal(expectedParameter10, passedParameter10);
            Assert.Equal(expectedParameter11, passedParameter11);
            Assert.Equal(expectedParameter12, passedParameter12);
            Assert.Equal(expectedParameter13, passedParameter13);
            Assert.Equal(expectedParameter14, passedParameter14);
            Assert.Equal(expectedParameter15, passedParameter15);
            Assert.Equal(expectedParameter16, passedParameter16);
        }

        private decimal SampleFunction(int parameter1, string parameter2, bool parameter3, decimal parameter4, byte parameter5, int parameter6, string parameter7, bool parameter8, decimal parameter9, byte parameter10, int? parameter11, string parameter12, bool? parameter13, decimal? parameter14, byte? parameter15, object parameter16)
        {
            return 42m;
        }
    }
}
