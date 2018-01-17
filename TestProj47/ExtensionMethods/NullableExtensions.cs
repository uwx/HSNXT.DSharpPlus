// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.NullableExtensions
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>Returns a formatted string for a nullable type.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">a nullable struct</param>
        /// <param name="format">string format.</param>
        /// <returns>Null if value is null; otherwise, returns a formatted string. </returns>
        public static string ToFormat<T>(this T? value, string format) where T : struct, IFormattable
        {
            if (!value.HasValue)
                return null;
            return value.Value.ToString(format, null);
        }
    }
}