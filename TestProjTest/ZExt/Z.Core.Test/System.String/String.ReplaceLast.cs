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
    public class System_String_ReplaceLast
    {
        [TestMethod]
        public void ReplaceLast()
        {
            // Type
            var @this = "zzzzz";

            // Exemples
            var result1 = @this.ReplaceLast("z", "a"); // return "zzzza";
            var result2 = @this.ReplaceLast(3, "z", "a"); // return "zzaaa";
            var result3 = @this.ReplaceLast(3, "z", "za"); // return "zzzazaza";
            var result4 = @this.ReplaceLast(4, "z", "a"); // return "zaaaa";
            var result5 = @this.ReplaceLast(5, "z", "a"); // return "aaaaa";
            var result6 = @this.ReplaceLast(10, "z", "a"); // return "aaaaa";

            // Unit Test
            Assert.AreEqual("zzzza", result1);
            Assert.AreEqual("zzaaa", result2);
            Assert.AreEqual("zzzazaza", result3);
            Assert.AreEqual("zaaaa", result4);
            Assert.AreEqual("aaaaa", result5);
            Assert.AreEqual("aaaaa", result6);
        }
    }
}