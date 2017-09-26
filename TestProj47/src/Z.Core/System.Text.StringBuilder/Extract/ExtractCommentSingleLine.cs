﻿// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System.Text;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A StringBuilder extension method that extracts the comment single line described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted comment single line.</returns>
        public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this)
        {
            return @this.ExtractCommentSingleLine(0);
        }

        /// <summary>
        ///     A StringBuilder extension method that extracts the comment single line described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="endIndex">[out] The end index.</param>
        /// <returns>The extracted comment single line.</returns>
        public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, out int endIndex)
        {
            return @this.ExtractCommentSingleLine(0, out endIndex);
        }

        /// <summary>
        ///     A StringBuilder extension method that extracts the comment single line described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The extracted comment single line.</returns>
        public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, int startIndex)
        {
            int endIndex;
            return @this.ExtractCommentSingleLine(startIndex, out endIndex);
        }

        /// <summary>
        ///     A StringBuilder extension method that extracts the comment single line described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">[out] The end index.</param>
        /// <returns>The extracted comment single line.</returns>
        public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, int startIndex, out int endIndex)
        {
            var sb = new StringBuilder();

            if (@this.Length > startIndex + 1)
            {
                var ch1 = @this[startIndex];
                var ch2 = @this[startIndex + 1];

                if (ch1 == '/' && ch2 == '/')
                {
                    // Single line comment

                    sb.Append(ch1);
                    sb.Append(ch2);
                    var pos = startIndex + 2;

                    while (pos < @this.Length)
                    {
                        var ch = @this[pos];
                        pos++;

                        if (ch == '\r' && pos < @this.Length && @this[pos] == '\n')
                        {
                            endIndex = pos - 1;
                            return sb;
                        }

                        sb.Append(ch);
                    }

                    endIndex = pos;
                    return sb;
                }
            }

            endIndex = -1;
            return null;
        }
    }
}