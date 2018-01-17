// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Data.Test
{
    [TestClass]
    public class System_Data_DataTable_ToExpandoObjects
    {
        [TestMethod]
        public void ToExpandoObjects()
        {
            // Type
            var @this = new DataTable();

            // Variables
            @this.Columns.Add("IntColumn", typeof (int));
            @this.Columns.Add("StringColumn", typeof (string));
            @this.Rows.Add(1, "Fizz");
            @this.Rows.Add(2, "Buzz");

            // Exemples
            var expandoObjects = @this.ToExpandoObjects().ToList();

            // Unit Test
            Assert.AreEqual(2, expandoObjects.Count);
            Assert.AreEqual(1, expandoObjects[0].IntColumn);
            Assert.AreEqual("Fizz", expandoObjects[0].StringColumn);
            Assert.AreEqual(2, expandoObjects[1].IntColumn);
            Assert.AreEqual("Buzz", expandoObjects[1].StringColumn);
        }
    }
}