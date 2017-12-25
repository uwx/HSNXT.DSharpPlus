using System.Collections.Generic;
using BCLExtensions.Tests.TestHelpers;
using Xunit; using HSNXT;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class IsNotEmptyTests
    {
        [Theory]
        [EnumerablePermutationData]
        public void WhenEmptyThenReturnsTrue<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = dataProvider.GetEmptyEnumerable();
            var result = input.IsNotEmpty();
            Assert.False(result);
        }

        [Theory]
        [EnumerablePermutationData]
        public void WhenNonEmptyThenReturnsFalse<T>(T resolutionPlaceholder, IDataProvider<T> dataProvider)
        {
            IEnumerable<T> input = dataProvider.GetEnumerableWithOneNonNullItem();
            var result = input.IsNotEmpty();
            Assert.True(result);
        }
    }
}