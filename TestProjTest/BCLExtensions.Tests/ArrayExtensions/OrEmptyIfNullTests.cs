using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ArrayExtensions
{
    public class OrEmptyIfNullTests
    {
        [Fact]
        public void EmptyArrayReturnsOriginalArray()
        {
            var array = new int[0];

            var result = array.OrEmptyIfNull();

            Assert.Same(array, result);
        }

        [Fact]
        public void NonEmptyArrayReturnsOriginalArray()
        {
            var array = new int[1];
            array[0] = 42;

            var result = array.OrEmptyIfNull();

            Assert.Same(array, result);
        }

        [Fact]
        public void NullArrayReferenceReturnsAnEmptyArray()
        {
            object[] array = null;

            var result = array.OrEmptyIfNull();

            Assert.NotNull(result);
            Assert.Equal(0,result.Length);
        }
    }
}
