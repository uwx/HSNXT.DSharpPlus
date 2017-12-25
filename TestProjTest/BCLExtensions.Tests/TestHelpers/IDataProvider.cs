using System.Collections.Generic;

namespace BCLExtensions.Tests.TestHelpers
{
    public interface IDataProvider<out T> : IDataProvider
    {
        IEnumerable<T> GetEmptyEnumerable();

        IEnumerable<T> GetEnumerableWithOneNonNullItem();
    }

    public interface IDataProvider
    {
        object GetPlaceholder();
    }
}