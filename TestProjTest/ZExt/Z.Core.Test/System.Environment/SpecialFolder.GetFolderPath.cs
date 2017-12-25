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
    public class System_Environement_SpecialFolder_GetFolderPath
    {
        [TestMethod]
        public void ToMoney()
        {
            // Type
            const Environment.SpecialFolder specialFolder = Environment.SpecialFolder.Desktop;

            // Exemples
            string path = specialFolder.GetFolderPath();

            // Unit Test
            Assert.AreEqual(Environment.GetFolderPath(specialFolder), path);
        }
    }
}