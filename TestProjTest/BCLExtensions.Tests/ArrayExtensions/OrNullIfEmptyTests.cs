using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ArrayExtensions
{
    public class OrNullIfEmptyTests
    {
        [Fact]
        public void EmptyArrayReturnsNull()
        {
            var array = new int[0];

            var result = array.OrNullIfEmpty();

            Assert.Null(result);
        }

        [Fact]
        public void NonEmptyArrayReturnsOriginalArray()
        {
            var array = new int[1];
            array[0] = 42;

            var result = array.OrNullIfEmpty();

            Assert.Same(array, result);
        }

        [Fact]
        public void NonEmptyArrayWithOnlyNullsReturnsNull()
        {
            var array = new object[1];

            var result = array.OrNullIfEmpty();

            Assert.Null(result);
        }

        [Fact]
        public void NullArrayReferenceReturnsNull()
        {
            object[] array = null;

            var result = array.OrNullIfEmpty();

            Assert.Null(result);
        }
    }
}
