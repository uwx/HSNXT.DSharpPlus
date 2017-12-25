using System.Collections.Generic;

namespace BCLExtensions.Tests.TestHelpers
{
    public class DictionaryProvider<T> : IDataProvider<KeyValuePair<object, T>>
    {
        private readonly IItemProvider<T> _provider;

        public DictionaryProvider(IItemProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<KeyValuePair<object, T>> GetEmptyEnumerable()
        {
            return new Dictionary<object, T>();
        }

        public IEnumerable<KeyValuePair<object, T>> GetEnumerableWithOneNonNullItem()
        {
            return new Dictionary<object, T> {{new object(), _provider.CreateItem()}};
        }

        public object GetPlaceholder()
        {
            return new KeyValuePair<object, T>(new object(), _provider.CreateItem());
        }
    }
}