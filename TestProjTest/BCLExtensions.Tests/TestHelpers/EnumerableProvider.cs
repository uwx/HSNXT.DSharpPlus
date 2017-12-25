using System.Collections.Generic;
using System.Linq;

namespace BCLExtensions.Tests.TestHelpers
{
    public class EnumerableProvider<T> : IDataProvider<T>
    {
        private readonly IItemProvider<T> _provider;

        public EnumerableProvider(IItemProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<T> GetEmptyEnumerable()
        {
            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> GetEnumerableWithOneNonNullItem()
        {
            return Enumerable.Range(1, 1).Select(n => _provider.CreateItem());
        }

        public object GetPlaceholder()
        {
            return _provider.CreateItem();
        }
    }
}