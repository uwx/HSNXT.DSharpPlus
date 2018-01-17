// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Reflection.Test
{
    [TestClass]
    public class System_Object_IsTypeOrInheritsOf
    {
        [TestMethod]
        public void IsTypeOrInheritsOf()
        {
            // Type
            var @this = new C();

            // Exemples
            var result1 = @this.IsTypeOrInheritsOf(typeof (C)); // return true;
            var result2 = @this.IsTypeOrInheritsOf(typeof (B)); // return true;
            var result3 = @this.IsTypeOrInheritsOf(typeof (A)); // return true;
            var result4 = @this.IsTypeOrInheritsOf(typeof (string)); // return false;

            // Unit Test
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.IsFalse(result4);
        }

        public class A
        {
        }

        public class B : A
        {
        }

        public class C : B
        {
        }
    }
}