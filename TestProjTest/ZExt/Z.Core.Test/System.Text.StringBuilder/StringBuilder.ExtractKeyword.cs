// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_Text_StringBuilder_ExtractKeyword
    {
        [TestMethod]
        public void ExtractKeyword()
        {
            int endIndex;

            // Unit Test
            Assert.AreEqual(null, new StringBuilder(" ").ExtractKeyword());
            Assert.AreEqual(null, new StringBuilder("1.1").ExtractKeyword());
            Assert.AreEqual(null, new StringBuilder("@1a").ExtractKeyword());
            Assert.AreEqual("for", new StringBuilder("for foreach").ExtractKeyword().ToString());
            Assert.AreEqual("@for", new StringBuilder("zzz @for foreach").ExtractKeyword(4, out endIndex).ToString());
            Assert.AreEqual(7, endIndex);
        }
    }
}