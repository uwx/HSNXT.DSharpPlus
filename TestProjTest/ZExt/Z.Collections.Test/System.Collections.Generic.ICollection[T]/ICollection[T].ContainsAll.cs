// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Collections.Test
{
    [TestClass]
    public class System_Collections_Generic_ICollection_T_ContainsAll
    {
        [TestMethod]
        public void ContainsAll()
        {
            // Type
            var @this = new List<string> {"zA", "zB", "C"};

            // Exemples
            var value1 = @this.ContainsAll("zA", "zB"); // return true;
            var value2 = @this.ContainsAll("zA", "2"); // return false;

            // Unit Test
            Assert.IsTrue(value1);
            Assert.IsFalse(value2);
        }
    }
}