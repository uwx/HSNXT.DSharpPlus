// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;
using System.Reflection;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method that gets property value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property value.</returns>
        public static object GetPropertyValue<T>(this T @this, string propertyName)
        {
            Type type = @this.GetType();
            PropertyInfo property = type.GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            return property.GetValue(@this, null);
        }
    }
}