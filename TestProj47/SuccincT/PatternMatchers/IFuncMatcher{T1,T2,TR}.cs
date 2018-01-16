using System;
using HSNXT.SuccincT.Unions;

namespace HSNXT.SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, T2, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> With(Either<T1, Any> value1,
                                                                              Either<T2, Any> value2);

        IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> Where(Func<T1, T2, bool> expression);
        IFuncMatcherAfterElse<TResult> Else(Func<T1, T2, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult result);
        TResult Result();
    }
}