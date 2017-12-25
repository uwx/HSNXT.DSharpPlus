using System.Collections.Generic;

namespace BCLExtensions.Tests.TestHelpers
{
    public class ArrayProvider<T> : IDataProvider<T>
    {
        private readonly IItemProvider<T> _provider;

        public ArrayProvider(IItemProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<T> GetEmptyEnumerable()
        {
            return new T[0];
        }

        public IEnumerable<T> GetEnumerableWithOneNonNullItem()
        {
            return new[] {_provider.CreateItem()};
        }

        public object GetPlaceholder()
        {
            return _provider.CreateItem();
        }
    }
}