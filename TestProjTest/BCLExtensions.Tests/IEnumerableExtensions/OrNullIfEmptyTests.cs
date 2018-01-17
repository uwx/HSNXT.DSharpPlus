using System.Collections.Generic;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class OrNullIfEmptyTests
    {
        [Theory]
        [EnumerablePermutationData]
        public void WhenNullThenReturnsNull<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = null;
            var result = input.OrNullIfEmpty();
            Assert.Null(result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenEmptyThenReturnsNull<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            var input = dataProvider.GetEmptyEnumerable();
            var result = input.OrNullIfEmpty();
            Assert.Null(result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenNonEmptyThenReturnsOriginalReference<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            var input = dataProvider.GetEnumerableWithOneNonNullItem();
            var result = input.OrNullIfEmpty();
            Assert.Equal(input, result);
        }
    }
}