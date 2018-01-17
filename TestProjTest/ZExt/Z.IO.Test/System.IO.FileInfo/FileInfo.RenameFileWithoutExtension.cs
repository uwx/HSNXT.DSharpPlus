// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.IO.Test
{
    [TestClass]
    public class System_IO_FileInfo_RenameFileWithoutExtension
    {
        [TestMethod]
        public void RenameFileWithoutExtension()
        {
            // Type
            var workingDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System_IO_FileInfo_Rename"));
            workingDirectory.EnsureDirectoryExists();
            workingDirectory.Clear();

            var @this = new FileInfo(Path.Combine(workingDirectory.FullName, "Examples_System_IO_FileInfo_RenameWithoutExtension.txt"));
            var @thisNewFile = new FileInfo(Path.Combine(workingDirectory.FullName, "Examples_System_IO_FileInfo_RenameWithoutExtension2.txt"));
            var result1 = @thisNewFile.Exists;

            // Intialization
            using (var stream = @this.Create())
            {
            }

            // Examples
            @this.RenameFileWithoutExtension("Examples_System_IO_FileInfo_RenameWithoutExtension2");

            // Unit Test
            @thisNewFile = new FileInfo(Path.Combine(workingDirectory.FullName, "Examples_System_IO_FileInfo_RenameWithoutExtension2.txt"));
            var result2 = @thisNewFile.Exists;

            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }
    }
}