// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Enumerates from @this to toCharacter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="toCharacter">to character.</param>
        /// <returns>An enumerator that allows foreach to be used to process @this to toCharacter.</returns>
        public static IEnumerable<char> To(this char @this, char toCharacter)
        {
            bool reverseRequired = (@this > toCharacter);

            char first = reverseRequired ? toCharacter : @this;
            char last = reverseRequired ? @this : toCharacter;

            IEnumerable<char> result = Enumerable.Range(first, last - first + 1).Select(charCode => (char) charCode);

            if (reverseRequired)
            {
                result = result.Reverse();
            }


            return result;
        }
    }
}