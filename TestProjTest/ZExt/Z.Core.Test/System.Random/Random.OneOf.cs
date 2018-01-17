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
    public class System_Random_OneOf
    {
        [TestMethod]
        public void OneOf()
        {
            // Type
            var @this = new Random();

            // Examples
            var value1 = @this.OneOf(1, 2, 3, 4); // return one of this value at random.
            var value2 = @this.OneOf("a", "b", "c", "d"); // return one of this value at random.
            var value3 = @this.OneOf(1, "a", DateTime.Now, new object()); // return one of this value at random.
        }
    }
}