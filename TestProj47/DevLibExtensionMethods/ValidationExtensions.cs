// Decompiled with JetBrains decompiler
// Type: TestProj47.ValidationExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Requires the argument not null.</summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void RequireArgumentNotNull<T>(this T source, string argumentName) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>Requires the argument not empty or whitespace.</summary>
        /// <param name="source">The source.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void RequireArgumentNotEmptyOrWhitespace(this string source, string argumentName)
        {
            if (source != null && source.IsNullOrWhiteSpace())
                throw new ArgumentException(argumentName + " can't be empty or contains only whitespace characters");
        }

        /// <summary>Requires the argument not null and not empty.</summary>
        /// <param name="source">The source.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void RequireArgumentNotNullAndNotEmpty(this string source, string argumentName)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>Requires the argument not null and not white space.</summary>
        /// <param name="source">The source.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void RequireArgumentNotNullAndNotWhiteSpace(this string source, string argumentName)
        {
            if (source.IsNullOrWhiteSpace())
                throw new ArgumentException(argumentName +
                                            " can't be null, empty or contains only whitespace characters");
        }

        /// <summary>Requires the argument not empty.</summary>
        /// <param name="source">The source.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void RequireArgumentNotEmpty(this Guid source, string argumentName)
        {
            if (source == Guid.Empty)
                throw new ArgumentException(argumentName + " can't be empty");
        }
    }
}