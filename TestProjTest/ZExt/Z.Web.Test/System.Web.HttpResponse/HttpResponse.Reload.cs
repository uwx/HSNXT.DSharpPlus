// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Web.Test
{
    [TestClass]
    public class System_Web_HttpResponse_Reload
    {
        [TestMethod]
        public void Reload()
        {
            var writer = new StringWriter();

            // Type
            var @this = new HttpResponse(writer);

            // Examples
            try
            {
                @this.Reload();
            }
            catch
            {
            }
        }
    }
}