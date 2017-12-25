using System.Collections.Generic;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class IsNullOrEmptyTests
    {
        [Theory]
        [EnumerablePermutationData]
        public void WhenNullThenReturnsTrue<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = null;
            var result = input.IsNullOrEmpty();
            Assert.True(result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenEmptyThenReturnsTrue<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = dataProvider.GetEmptyEnumerable();
            var result = input.IsNullOrEmpty();
            Assert.True(result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenNonEmptyThenReturnsFalse<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = dataProvider.GetEnumerableWithOneNonNullItem();
            var result = input.IsNullOrEmpty();
            Assert.False(result);
        }
    }
}
