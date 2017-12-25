// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Compression.Test
{
    [TestClass]
    public class System_IO_FileInfo_CreateGZip
    {
        [TestMethod]
        public void CreateGZip()
        {
            // Type
            var @this = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples_System_IO_FileInfo_CreateGZip.txt"));
            var output = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples_System_IO_FileInfo_CreateGZip.gz"));

            // Intialization
            using (FileStream stream = @this.Create())
            {
            }

            // Examples
            @this.CreateGZip(output);

            // Unit Test
            Assert.IsTrue(output.Exists);
        }
    }
}