namespace BCLExtensions.Tests.TestHelpers
{
    public static class FuncHelpers
    {
        public static bool ReturnFalse<T>(T input)
        {
            return false;
        }

        public static T SelectSelf<T>(T v)
        {
            return v;
        }
    }
}
