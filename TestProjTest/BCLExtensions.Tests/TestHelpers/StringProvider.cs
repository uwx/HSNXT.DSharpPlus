namespace BCLExtensions.Tests.TestHelpers
{
    public class StringProvider : IItemProvider<string>
    {
        public string CreateItem()
        {
            return "Test string";
        }
    }
}