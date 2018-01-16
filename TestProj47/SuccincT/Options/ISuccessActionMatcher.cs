using System;
using HSNXT.SuccincT.Unions.PatternMatchers;

namespace HSNXT.SuccincT.Options
{
    public interface ISuccessActionMatcher<T>
    {
        IUnionActionPatternCaseHandler<ISuccessActionMatcher<T>, T> Error();

        ISuccessActionMatchHandler<T> Success();

        IUnionActionPatternMatcherAfterElse Else(Action<Success<T>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}