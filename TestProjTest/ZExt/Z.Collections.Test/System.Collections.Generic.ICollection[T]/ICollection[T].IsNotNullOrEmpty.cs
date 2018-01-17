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
    public class System_Collections_Generic_ICollection_T_IsNotNullOrEmpty
    {
        [TestMethod]
        public void IsNotNullOrEmpty()
        {
            // Type
            var @this = new List<string>();

            // Examples
            var value1 = @this.IsNotNullOrEmpty(); // return false;

            @this.Add("Fizz");
            var value2 = @this.IsNotNullOrEmpty(); // return true;

            @this = null;
            var value3 = @this.IsNotNullOrEmpty(); // return false;

            // Unit Test
            Assert.IsFalse(value1);
            Assert.IsTrue(value2);
            Assert.IsFalse(value3);
        }
    }
}