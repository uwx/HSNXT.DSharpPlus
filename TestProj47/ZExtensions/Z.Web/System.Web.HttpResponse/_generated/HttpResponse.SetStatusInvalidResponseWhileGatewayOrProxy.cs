// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System.Web;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 502 (Web server received an invalid
        ///     response while acting as a gateway or proxy. ).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusInvalidResponseWhileGatewayOrProxy(this HttpResponse @this)
        {
            @this.StatusCode = 502;
            @this.StatusDescription = "Web server received an invalid response while acting as a gateway or proxy. ";
        }
    }
}