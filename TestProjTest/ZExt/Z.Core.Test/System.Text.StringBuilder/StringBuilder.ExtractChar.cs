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
    public class System_Text_StringBuilder_ExtractChar
    {
        [TestMethod]
        public void ExtractChar()
        {
            int endIndex;

            // Unit Test
            Assert.AreEqual('a', new StringBuilder("'a'").ExtractChar());
            Assert.AreEqual('\'', new StringBuilder("'\''").ExtractChar());
            Assert.AreEqual('\'', new StringBuilder("z'\''").ExtractChar(1, out endIndex));
            Assert.AreEqual(3, endIndex);

            try
            {
                new StringBuilder("'").ExtractChar();
                throw new Exception("invalid!");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid char at position: 0", ex.Message);
            }
        }
    }
}