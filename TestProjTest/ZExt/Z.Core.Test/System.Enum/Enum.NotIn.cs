// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_Enum_NotIn
    {
        [TestMethod]
        public void NotIn()
        {
            // Type
            var @this = Environment.SpecialFolder.Desktop;

            // Exemples
            var result1 = @this.NotIn(Environment.SpecialFolder.Desktop, Environment.SpecialFolder.DesktopDirectory); // return false;
            var result2 = @this.NotIn(Environment.SpecialFolder.DesktopDirectory); // return true;

            // Unit Test
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }
    }
}