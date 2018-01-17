using System.Collections.Generic;
using System.Linq;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class OrEmptyIfNullTests
    {
        [Theory]
        [EnumerablePermutationData]
        public void WhenNullThenReturnsEmptyEnumerable<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = null;
            var result = input.OrEmptyIfNull();
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenEmptyThenReturnsOriginalReference<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            var input = dataProvider.GetEmptyEnumerable();
            var result = input.OrEmptyIfNull();
            Assert.Equal(input, result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenNonEmptyThenReturnsOriginalReference<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            var input = dataProvider.GetEnumerableWithOneNonNullItem();
            var result = input.OrEmptyIfNull();
            Assert.Equal(input, result);
        }
    }
}