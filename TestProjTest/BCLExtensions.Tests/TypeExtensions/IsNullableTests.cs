using System; using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.TypeExtensions
{
    public class IsNullableTests
    {
        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(object))]
        [InlineData(typeof(byte?))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(float?))]
        [InlineData(typeof(double?))]
        [InlineData(typeof(decimal?))]
        public void IntIsNullableReturnsTrue(Type type)
        {
            var isIntNullable = type.IsNullable();

            Assert.True(isIntNullable);
        }

        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(int))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        public void IntIsNullableReturnsFalse(Type type)
        {
            var isIntNullable = type.IsNullable();

            Assert.False(isIntNullable);
        }
    }
}
