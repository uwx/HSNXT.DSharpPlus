namespace HSNXT.SuccincT.PatternMatchers
{
    public interface IMapperNoneHandler<T, TResult>
    {
        IMapperMatcher<T, TResult> Do(TResult doValue);
    }
}