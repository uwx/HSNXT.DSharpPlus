// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.IO.Test
{
    [TestClass]
    public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
    {
        [TestMethod]
        public void EnumerateFileSystemEntries()
        {
            // Type
            var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System_IO_DirectoryInfo_EnumerateFileSystemEntries"));
            Directory.CreateDirectory(root.FullName);

            var file1 = new FileInfo(Path.Combine(root.FullName, "test1.txt"));
            file1.Create();
            root.CreateSubdirectory("test2");

            // Exemples
            List<string> result = root.EnumerateFileSystemEntries().ToList();

            // Unit Test
            Assert.AreEqual(2, result.Count);
        }
    }
}