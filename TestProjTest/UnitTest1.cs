using System.Collections.Generic;
using FluentAssertions;
using HSNXT;
using Xunit;

namespace TestProjTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var list = new List<string>(new[] { "1", "2", "3" });

            (var a, (var b, var rest)) = list;
            
            a.ShouldBeEquivalentTo("1");
            b.ShouldBeEquivalentTo("2");
            rest.GetHead().ShouldBeEquivalentTo("3");
        }
    }
}