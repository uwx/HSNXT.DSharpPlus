namespace BCLExtensions.Tests.TestHelpers
{
    public class IntProvider : IItemProvider<int>
    {
        public int CreateItem()
        {
            return 42;
        }
    }
}