﻿using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.FuncExtensions
{
    public class AsActionUsingWithSevenParametersTests
    {
        [Fact]
        public void SampleFunctionIsValid()
        {
            SampleFunction(42, "Test", true, 9.876m, byte.MaxValue, 12, "Foo");
        }

        [Fact]
        public void ResultNotNull()
        {
            Func<int, string, bool, decimal, byte, int, string, decimal> function = SampleFunction;

            var action = function.AsActionUsing(12, "12", false, 5.4321m, byte.MaxValue, 42, "Bar");

            Assert.NotNull(action);
        }

        [Fact]
        public void InternalFunctionExecutes()
        {
            bool internalFunctionWasCalled = false;
            Func<int, string, bool, decimal, byte, int, string, decimal> function = (p1,p2,p3,p4,p5,p6,p7) =>
            {
                internalFunctionWasCalled = true;
                return 42m;
            };
            var action = function.AsActionUsing(12,"24",true, 3.14m, byte.MaxValue, 16, "Fizz");
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
            int passedParameter1 = 0;
            string passedParameter2 = null;
            bool passedParameter3 = false;
            decimal passedParameter4 = 0.0m;
            byte passedParameter5 = 0;
            int passedParameter6 = 0;
            string passedParameter7 = null;
            Func<int, string, bool, decimal, byte, int, string, decimal> function = (p1,p2,p3,p4,p5,p6,p7) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
                passedParameter4 = p4;
                passedParameter5 = p5;
                passedParameter6 = p6;
                passedParameter7 = p7;
                return 42;
            };

            var action = function.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3, expectedParameter4, expectedParameter5, expectedParameter6, expectedParameter7);
            action();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
            Assert.Equal(expectedParameter4, passedParameter4);
            Assert.Equal(expectedParameter5, passedParameter5);
            Assert.Equal(expectedParameter6, passedParameter6);
            Assert.Equal(expectedParameter7, passedParameter7);
        }

        private decimal SampleFunction(int parameter1, string parameter2, bool parameter3, decimal parameter4, byte parameter5, int parameter6, string parameter7)
        {
            return 42m;
        }
    }
}
