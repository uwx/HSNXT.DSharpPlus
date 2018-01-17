// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.ExceptionExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Runtime.ExceptionServices;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static ExceptionDispatchInfo UnrollDynamicallyInvokedException(this Exception ex)
        {
            if (ex == null)
                throw new NullReferenceException("UnrollDynamicallyInvokedException given null exception");
            var innerException = ex.InnerException;
            while (innerException.InnerException != null)
                innerException = innerException.InnerException;
            if (innerException != null)
                return ExceptionDispatchInfo.Capture(innerException);
            return ExceptionDispatchInfo.Capture(ex);
        }
    }
}