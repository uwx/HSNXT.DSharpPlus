using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HSNXT;
using System.Linq;

namespace ComLib.Test
{
    [TestClass]
    public class ListExtensionTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            var test = new List<int> {1, 2};

            var result = test.ToString<int>();

            Assert.IsTrue(result.Equals("List`1 { 1 , 2 }"));
        }

        [TestMethod]
        public void DistinctByTest()
        {
            var test = new List<TestClass>();
            test.Add(new TestClass() { TestInt = 1 });
            test.Add(new TestClass() { TestInt = 1 });
            test.Add(new TestClass() { TestInt = 2 });
            test.Add(new TestClass() { TestInt = 2 });
            test.Add(new TestClass() { TestInt = 3 });
            test.Add(new TestClass() { TestInt = 3 });
            test.Add(new TestClass() { TestInt = 4 });
            test.Add(new TestClass() { TestInt = 4 });

            var distinct = test.DistinctBy(i => i.TestInt).OrderBy(i => i.TestInt).ToList();
            var count = distinct.Count();

            // The distinct should get unique TestInt making it 1, 2, 3, 4 and thats it no more.
            Assert.IsTrue(count == 4);
        }
    }
}
