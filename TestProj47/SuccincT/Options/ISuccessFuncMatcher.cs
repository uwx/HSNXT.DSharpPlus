using System;
using HSNXT.SuccincT.Unions.PatternMatchers;

namespace HSNXT.SuccincT.Options
{
    public interface ISuccessFuncMatcher<T, TResult>
    {
        IUnionFuncPatternCaseHandler<ISuccessFuncMatcher<T, TResult>, T, TResult> Error();
        ISuccessFuncMatchHandler<T, TResult> Success();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult elseValue);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Success<T>, TResult> elseAction);

        TResult Result();
    }
}