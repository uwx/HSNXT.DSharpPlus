using System; using HSNXT;
using Xunit;
using HSNXT.BCLExtensions2;

namespace BCLExtensions.Tests.ActionExtensions
{
    public class AsFuncTests
    {
        [Fact]
        public void SampleActionIsValid()
        {
            SampleAction();
        }

        [Fact]
        public void ResultNotNull()
        {
            Action action = SampleAction;

            var func = action.AsFunc();

            Assert.NotNull(func);
        }

        [Fact]
        public void ResultExecutesCorrectly()
        {
            Action action = SampleAction;

            var func = action.AsFunc();

            var unit = func();
            Assert.Equal(Unit.Default, unit);
        }

        private void SampleAction()
        {
        }
    }
}
