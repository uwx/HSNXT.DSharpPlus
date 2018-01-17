// Decompiled with JetBrains decompiler
// Type: TestProj47.BoolExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>
        /// Converts the value of this instance to its equivalent string representation (either "Yes" or "No").
        /// </summary>
        /// <param name="source">Source bool.</param>
        /// <returns>"Yes" or "No" string.</returns>
        public static string ToYesNoString(this bool source)
        {
            return !source ? "No" : "Yes";
        }

        /// <summary>Converts the value in number format {1 , 0}.</summary>
        /// <param name="source">Source bool.</param>
        /// <returns>1 if true; otherwise, 0.</returns>
        public static int ToBitInt(this bool source)
        {
            return !source ? 0 : 1;
        }

        /// <summary>Converts the value in number format {1 , 0}.</summary>
        /// <param name="source">Source bool.</param>
        /// <returns>"1" if true; otherwise, "0".</returns>
        public static string ToBitString(this bool source)
        {
            return !source ? "0" : "1";
        }

        /// <summary>Convert 0 or 1 to bool.</summary>
        /// <param name="source">Source int.</param>
        /// <returns>true if 1; otherwise, false.</returns>
        public static bool BitIntToBool(this int source)
        {
            return source != 0;
        }

        /// <summary>Convert "0" or "1" to bool.</summary>
        /// <param name="source">Source int.</param>
        /// <returns>true if "1"; otherwise, false.</returns>
        public static bool BitStringToBool(this string source)
        {
            return source != "0";
        }

        /// <summary>Convert "Yes" or "No" to bool.</summary>
        /// <param name="source">Source string.</param>
        /// <param name="ignoreCase">Indicating a case-sensitive or insensitive comparison.</param>
        /// <returns>true if "Yes"; otherwise, false.</returns>
        public static bool YesNoToBool(this string source, bool ignoreCase = true)
        {
            return "Yes".Equals(source, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }
    }
}