// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.Helpers.Check
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System;

namespace HSNXT.Helpers
{
    internal static class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (!value.HasValue)
                throw new ArgumentNullException(parameterName);
            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"Argument {(object) parameterName} is null or whitespace");
            return value;
        }
    }
}