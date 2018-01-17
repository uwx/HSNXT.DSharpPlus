// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Reflection.Test
{
    [TestClass]
    public class Constructor_GetSignature_GenericTypeConstructor
    {
        [TestMethod]
        public void GetSignature()
        {
            // Type
            var @this = typeof (GenericTypeConstructorModel<>).GetConstructors()[0];

            // Exemples
            var result = @this.GetSignature();

            // Unit Test
            Assert.AreEqual("GenericTypeConstructorModel(T)", result);
        }
    }
}