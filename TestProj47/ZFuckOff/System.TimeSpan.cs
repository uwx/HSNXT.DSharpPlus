// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A TimeSpan extension method that substract the specified TimeSpan to the current DateTime.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The current DateTime with the specified TimeSpan substracted from it.</returns>
        public static DateTime Ago(this TimeSpan @this)
        {
            return DateTime.Now.Subtract(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A TimeSpan extension method that add the specified TimeSpan to the current DateTime.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The current DateTime with the specified TimeSpan added to it.</returns>
        public static DateTime FromNow(this TimeSpan @this)
        {
            return DateTime.Now.Add(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A TimeSpan extension method that substract the specified TimeSpan to the current UTC (Coordinated Universal
        ///     Time)
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The current UTC (Coordinated Universal Time) with the specified TimeSpan substracted from it.</returns>
        public static DateTime UtcAgo(this TimeSpan @this)
        {
            return DateTime.UtcNow.Subtract(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A TimeSpan extension method that add the specified TimeSpan to the current UTC (Coordinated Universal Time)
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The current UTC (Coordinated Universal Time) with the specified TimeSpan added to it.</returns>
        public static DateTime UtcFromNow(this TimeSpan @this)
        {
            return DateTime.UtcNow.Add(@this);
        }
    }
}