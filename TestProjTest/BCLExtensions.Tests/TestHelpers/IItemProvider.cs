namespace BCLExtensions.Tests.TestHelpers
{
    public interface IItemProvider<out T>
    {
        T CreateItem();
    }
}