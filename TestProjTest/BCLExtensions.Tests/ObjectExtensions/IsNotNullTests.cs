using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ObjectExtensions
{
    public class IsNotNullTests
    {
        [Fact]
        public void WhenInstanceIsNullReturnsFalse()
        {
            object instance = null;
            var result = instance.IsNotNull();

            Assert.False(result);
        }

        [Fact]
        public void WhenInstanceIsNotNullReturnsTrue()
        {
            var instance = new Object();
            var result = instance.IsNotNull();

            Assert.True(result);
        }
    }
}