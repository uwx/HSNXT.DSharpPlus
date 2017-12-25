using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BCLExtensions.Tests.TestHelpers
{
    public class CollectionProvider<T> : IDataProvider<T>
    {
        private readonly IItemProvider<T> _provider;

        public CollectionProvider(IItemProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<T> GetEmptyEnumerable()
        {
            return new Collection<T>();
        }

        public IEnumerable<T> GetEnumerableWithOneNonNullItem()
        {
            return new Collection<T> { _provider.CreateItem() };
        }

        public object GetPlaceholder()
        {
            return _provider.CreateItem();
        }
    }
}