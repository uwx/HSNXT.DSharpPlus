using System;
using Xunit;
using XunitShould;


namespace HSNXT.Tests
{
    public class ExtensionsTestFixture
    {
        [Fact]
        public void TrimToLengthShouldCutStringToSpecfiedLength()
        {
            string s = "123456";
            string x = s.TrimToLength(3);
            x.Length.ShouldEqual(3);
        }

        [Fact]
        public void ShowIf_ReturnsStringWhenFuncIsTrue()
        {
            var result = "This".ShowIf(() => true);

            result.ShouldEqual("This");
        }

        [Fact]
        public void ShowIf_ReturnsEmptyStringWhenFuncIsFalse()
        {
            var result = "This".ShowIf(() => false);

            result.ShouldEqual("");
        }
    }
}