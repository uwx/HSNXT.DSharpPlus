using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ArrayExtensions
{
    public class ClearTests
    {
        [Fact]
        public void EmptyArrayClearSuccessfull()
        {
            var array = new int[0];

            array.Clear();

            Assert.NotNull(array);
        }

        [Fact]
        public void ArrayWithOneDefaultOnlyStaysDefault()
        {
            var array = new int[1];

            array.Clear();

            Assert.Equal(default(int), array[0]);
        }

        [Fact]
        public void ArrayWithOneValueOnlyResetsToDefaultValue()
        {
            var array = new int[1];
            array[0] = 42;

            array.Clear();

            Assert.Equal(default(int), array[0]);
        }

        [Fact]
        public void ArrayWithOneValueOneDefaultResetsToDefaultForBoth()
        {
            var array = new int[2];
            array[0] = 42;

            array.Clear();

            Assert.Equal(default(int), array[0]);
            Assert.Equal(default(int), array[1]);
        }
    }
}
