// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Reflection.Test
{
    [TestClass]
    public class Property_GetSignature_GenericIndexerProperty
    {
        [TestMethod]
        public void GetSignature()
        {
            // Type
            PropertyInfo @this = typeof (PropertyModel<>).GetProperties().First(x => x.Name == "Item" && x.GetIndexParameters().Length == 2);

            // Exemples
            string result = @this.GetSignature();

            // Unit Test
            Assert.AreEqual("Item[T, int]", result);
        }
    }
}