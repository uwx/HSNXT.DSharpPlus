using System;

namespace HSNXT.SuccincT.Options
{
    public interface INoneActionMatchHandler<T>
    {
        IOptionActionMatcher<T> Do(Action action);
    }
}