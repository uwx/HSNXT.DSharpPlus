using System;

namespace HSNXT.SuccincT.Options
{
    public interface ISuccessActionMatchHandler<T>
    {
        ISuccessActionMatcher<T> Do(Action action);
    }
}