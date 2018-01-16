using System;
using HSNXT.SuccincT.Functional;

namespace HSNXT.SuccincT.PatternMatchers
{
    public interface IMapperRecursiveConsHandler<T, TResult>
    {
        IMapperRecursiveConsWhereHandler<T, TResult> Where(Func<T, T, bool> whereFunc);

        IMapperMatcher<T, TResult> Do(Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc);
    }
}