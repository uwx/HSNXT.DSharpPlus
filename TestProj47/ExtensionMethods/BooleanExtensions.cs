// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.BooleanExtensions
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Returns Yes or No string if a boolean value is true or false, respectively.
        /// </summary>
        /// <param name="b">value to test</param>
        /// <returns>Yes or No string.</returns>
        public static string ToYesOrNo(this bool b)
        {
            return !b ? "No" : "Yes";
        }
    }
}