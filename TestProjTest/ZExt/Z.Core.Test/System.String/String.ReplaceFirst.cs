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
    public class System_String_ReplaceFirst
    {
        [TestMethod]
        public void ReplaceFirst()
        {
            // Type
            var @this = "zzzzz";

            // Exemples
            var result1 = @this.ReplaceFirst("z", "a"); // return "azzzz";
            var result2 = @this.ReplaceFirst(3, "z", "a"); // return "aaazz";
            var result3 = @this.ReplaceFirst(3, "z", "za"); // return "zazazazz";
            var result4 = @this.ReplaceFirst(4, "z", "a"); // return "aaaaz";
            var result5 = @this.ReplaceFirst(5, "z", "a"); // return "aaaaa";
            var result6 = @this.ReplaceFirst(10, "z", "a"); // return "aaaaa";

            // Unit Test
            Assert.AreEqual("azzzz", result1);
            Assert.AreEqual("aaazz", result2);
            Assert.AreEqual("zazazazz", result3);
            Assert.AreEqual("aaaaz", result4);
            Assert.AreEqual("aaaaa", result5);
            Assert.AreEqual("aaaaa", result6);
        }
    }
}