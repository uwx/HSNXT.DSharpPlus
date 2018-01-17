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
    public class System_String_ExtractUInt16
    {
        [TestMethod]
        public void ExtractUInt16()
        {
            // Type

            // Exemples
            var result1 = "Fizz 123 Buzz".ExtractUInt16(); // return 123;
            var result2 = "Fizz -123 Buzz".ExtractUInt16(); // return 123;
            var result3 = "-Fizz 123 Buzz".ExtractUInt16(); // return 123;
            var result4 = "Fizz 123.4 Buzz".ExtractUInt16(); // return 1234;
            var result5 = "Fizz -123Fizz.Buzz4 Buzz".ExtractUInt16(); // return 1234;

            // Unit Test
            Assert.AreEqual((UInt16) 123, result1);
            Assert.AreEqual((UInt16) 123, result2);
            Assert.AreEqual((UInt16) 123, result3);
            Assert.AreEqual((UInt16) 1234, result4);
            Assert.AreEqual((UInt16) 1234, result5);
        }
    }
}