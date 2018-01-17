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
    public class System_DateTime_Age
    {
        [TestMethod]
        public void Age()
        {
            // Type
            var @this = new DateTime(1981, 01, 01);

            // Exemples
            var result = @this.Age(); // result CurrentYear - 1981

            // Unit Test
            Assert.AreEqual(DateTime.Now.Year - 1981, result);
        }
    }
}