﻿using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ActionExtensions
{

    public class AsActionUsingWithThreeParametersTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction(42, "Test", true);
        }

        [Fact]
        public void ResultNotNull()
        {
            Action<int, string, bool> action = SampleAction;

            var result = action.AsActionUsing(12, "12", false);

            Assert.NotNull(result);
        }

        [Fact]
        public void InternalActionExecutes()
        {
            bool internalActionWasCalled = false;
            Action<int, string, bool> action = (p1,p2,p3) =>
            {
                internalActionWasCalled = true;
            };
            var result = action.AsActionUsing(12,"24", false);
            result();

            Assert.True(internalActionWasCalled);
        }

        [Fact]
        public void InternalActionCapturesCorrectParameters()
        {
            const int expectedParameter1 = 12;
            const string expectedParameter2 = "24";
            const bool expectedParameter3 = true;
            int passedParameter1 = 0;
            string passedParameter2 = null;
            bool passedParameter3 = false;
            Action<int, string, bool> action = (p1,p2,p3) =>
            {
                passedParameter1 = p1;
                passedParameter2 = p2;
                passedParameter3 = p3;
            };

            var result = action.AsActionUsing(expectedParameter1, expectedParameter2, expectedParameter3);
            result();

            Assert.Equal(expectedParameter1, passedParameter1);
            Assert.Equal(expectedParameter2, passedParameter2);
            Assert.Equal(expectedParameter3, passedParameter3);
        }

        private void SampleAction(int parameter1, string parameter2, bool parameter3)
        {
        }

    }
}
