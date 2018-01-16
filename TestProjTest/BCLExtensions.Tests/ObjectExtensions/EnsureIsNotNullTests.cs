using HSNXT;
using Xunit;

namespace BCLExtensions.Tests.ObjectExtensions
{
    public class EnsureIsNotNullTests
    {
        private string instanceArgumentName = "instance";

        [Fact]
        public void WhenInstanceIsNotNullThenRunsSuccessfully()
        {
            var instance = new object();
            instance.EnsureIsNotNull();
        }

        [Fact]
        public void WhenInstanceIsNotNullWithNamedArgumentThenRunsSuccessfully()
        {
            var instance = new object();
            instance.EnsureIsNotNull(instanceArgumentName);
        }

    }
}
