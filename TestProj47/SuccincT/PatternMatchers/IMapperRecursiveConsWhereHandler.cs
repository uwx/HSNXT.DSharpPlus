using System;
using HSNXT.SuccincT.Functional;

namespace HSNXT.SuccincT.PatternMatchers
{
    public interface IMapperRecursiveConsWhereHandler<T, TResult>
    {
        IMapperMatcher<T, TResult> Do(Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc);
    }
}