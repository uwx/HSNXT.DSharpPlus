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
        /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted keyword.</returns>
        public static StringBuilder ExtractKeyword(this StringBuilder @this)
        {
            return @this.ExtractKeyword(0);
        }

        /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="endIndex">[out] The end index.</param>
        /// <returns>The extracted keyword.</returns>
        public static StringBuilder ExtractKeyword(this StringBuilder @this, out int endIndex)
        {
            return @this.ExtractKeyword(0, out endIndex);
        }

        /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The extracted keyword.</returns>
        public static StringBuilder ExtractKeyword(this StringBuilder @this, int startIndex)
        {
            int endIndex;
            return @this.ExtractKeyword(startIndex, out endIndex);
        }

        /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">[out] The end index.</param>
        /// <returns>The extracted keyword.</returns>
        public static StringBuilder ExtractKeyword(this StringBuilder @this, int startIndex, out int endIndex)
        {
            // WARNING: This method support custom operator for .NET Runtime Compiler
            // An operator can be any sequence of supported operator character
            var sb = new StringBuilder();

            var pos = startIndex;
            var hasCharacter = false;

            while (pos < @this.Length)
            {
                var ch = @this[pos];
                pos++;

                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    hasCharacter = true;
                    sb.Append(ch);
                }
                else if (ch == '@')
                {
                    sb.Append(ch);
                }
                else if (ch >= '0' && ch <= '9' && hasCharacter)
                {
                    sb.Append(ch);
                }
                else
                {
                    pos -= 2;
                    break;
                }
            }

            if (hasCharacter)
            {
                endIndex = pos;
                return sb;
            }

            endIndex = -1;
            return null;
        }
    }
}