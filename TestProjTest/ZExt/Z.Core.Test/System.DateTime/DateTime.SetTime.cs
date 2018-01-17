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
    public class System_DateTime_SetTime
    {
        [TestMethod]
        public void SetTime()
        {
            // Type
            var @thisToday = DateTime.Today;

            // Exemples
            var result = @thisToday.SetTime(15); // Set hours to 15

            // Unit Test
            Assert.AreEqual(15, result.Hour);
        }
    }
}