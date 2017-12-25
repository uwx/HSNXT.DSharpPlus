// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_Text_StringBuilder_Substring
    {
        [TestMethod]
        public void Substring()
        {
            // Type
            var @this = new StringBuilder("0123456789");

            // Exemples
            var result1 = @this.Substring(4); // return "456789"
            var result2 = @this.Substring(4, 2); // return "45"

            // Unit Test
            Assert.AreEqual("456789", result1);
            Assert.AreEqual("45", result2);
        }
    }
}