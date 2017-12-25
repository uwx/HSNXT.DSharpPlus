// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_Guid_IsNotEmpty
    {
        [TestMethod]
        public void IsNotEmpty()
        {
            // Type
            Guid guidEmpty = Guid.Empty;
            Guid guidValue = Guid.NewGuid();

            // Unit Test
            Assert.IsFalse(guidEmpty.IsNotEmpty());
            Assert.IsTrue(guidValue.IsNotEmpty());
        }
    }
}