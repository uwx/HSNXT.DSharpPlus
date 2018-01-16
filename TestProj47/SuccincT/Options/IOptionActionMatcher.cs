using System;
using HSNXT.SuccincT.Unions.PatternMatchers;

namespace HSNXT.SuccincT.Options
{
    public interface IOptionActionMatcher<T>
    {
        IUnionActionPatternCaseHandler<IOptionActionMatcher<T>, T> Some();

        INoneActionMatchHandler<T> None();

        IUnionActionPatternMatcherAfterElse Else(Action<Option<T>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}