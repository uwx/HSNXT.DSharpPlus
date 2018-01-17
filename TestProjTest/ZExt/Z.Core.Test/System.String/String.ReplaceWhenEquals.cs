// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_String_ReplaceWhenEquals
    {
        [TestMethod]
        public void ReplaceExact()
        {
            // Type
            var @this = "Fizz";

            // Exemples
            var result1 = @this.ReplaceWhenEquals("Fizz", "Buzz"); // return "Buzz";
            var result2 = @this.ReplaceWhenEquals("Fiz", "Buzz"); // return "Fizz";

            // Unit Test
            Assert.AreEqual("Buzz", result1);
            Assert.AreEqual("Fizz", result2);
        }
    }
}