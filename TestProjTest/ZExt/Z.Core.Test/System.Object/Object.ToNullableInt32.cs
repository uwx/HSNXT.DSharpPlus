// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Core.Test
{
    [TestClass]
    public class System_Object_ToNullableInt32
    {
        [TestMethod]
        public void ToNullableInt32()
        {
            // Type
            object @this = null;
            object @thisValue = "32";

            // Exemples
            int? result1 = @this.ToNullableInt32(); // return null;
            int? result2 = @thisValue.ToNullableInt32(); // return 32;

            // Unit Test
            Assert.IsFalse(result1.HasValue);
            Assert.AreEqual(32, result2.Value);
        }
    }
}