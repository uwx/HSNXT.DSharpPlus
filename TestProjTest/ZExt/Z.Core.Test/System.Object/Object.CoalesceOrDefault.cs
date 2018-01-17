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
    public class System_Object_CoalesceOrDefault
    {
        [TestMethod]
        public void CoalesceOrDefault()
        {
            // Varable
            object nullObject = null;

            // Type
            object @thisNull = null;
            object @thisNotNull = "Fizz";

            // Exemples
            var result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, "Buzz"); // return "Buzz";
            var result2 = @thisNull.CoalesceOrDefault(() => "Buzz", null, null); // return "Buzz";
            var result3 = @thisNull.CoalesceOrDefault(x => "Buzz", null, null); // return "Buzz";
            var result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, "Buzz"); // return "Fizz";

            // Unit Test
            Assert.AreEqual("Buzz", result1);
            Assert.AreEqual("Buzz", result2);
            Assert.AreEqual("Buzz", result3);
            Assert.AreEqual("Fizz", result4);
        }
    }
}