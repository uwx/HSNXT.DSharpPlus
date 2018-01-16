using System;
using HSNXT.SuccincT.PatternMatchers;

namespace HSNXT.SuccincT.Unions.PatternMatchers
{
    public interface IUnionActionPatternCaseHandler<out TMatcher, T>
    {
        TMatcher Do(Action<T> action);
        IActionWithHandler<TMatcher, T> Of(T value);
        IActionWhereHandler<TMatcher, T> Where(Func<T, bool> expression);
    }
}