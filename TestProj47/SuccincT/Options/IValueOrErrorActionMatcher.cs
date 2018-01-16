using System;
using HSNXT.SuccincT.Unions.PatternMatchers;

namespace HSNXT.SuccincT.Options
{
    public interface IValueOrErrorActionMatcher
    {
        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Value();

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Error();

        IUnionActionPatternMatcherAfterElse Else(Action<ValueOrError> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}