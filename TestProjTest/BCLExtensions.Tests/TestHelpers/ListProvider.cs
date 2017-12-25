using System.Collections.Generic;

namespace BCLExtensions.Tests.TestHelpers
{
    public class ListProvider<T> : IDataProvider<T>
    {
        private readonly IItemProvider<T> _provider;

        public ListProvider(IItemProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<T> GetEmptyEnumerable()
        {
            return new List<T>();
        }

        public IEnumerable<T> GetEnumerableWithOneNonNullItem()
        {
            return new List<T> {_provider.CreateItem()};
        }

        public object GetPlaceholder()
        {
            return _provider.CreateItem();
        }
    }
}