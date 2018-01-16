using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ObjectExtensions
{
    public class IsNullTests
    {
        [Fact]
        public void WhenInstanceIsNullReturnsTrue()
        {
            object instance = null;
            var result = instance.IsNull();

            Assert.True(result);
        }

        [Fact]
        public void WhenInstanceIsNotNullReturnsFalse()
        {
            var instance = new Object();
            var result = instance.IsNull();

            Assert.False(result);
        }
    }
}
