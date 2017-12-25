// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Reflection.Test
{
    [TestClass]
    public class Method_GetSignature_GenericTypeMethod
    {
        [TestMethod]
        public void GetSignature()
        {
            // Type
            var @this = typeof(MethodModel<>).GetMethod("GenericTypeMethod", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // Exemples
            string result = @this.GetSignature();

            // Unit Test
            Assert.AreEqual("GenericTypeMethod(T)", result);
        }
    }
}