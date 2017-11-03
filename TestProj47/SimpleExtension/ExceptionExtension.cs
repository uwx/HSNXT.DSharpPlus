// Decompiled with JetBrains decompiler
// Type: SimpleExtension.ExceptionExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\SimpleExtension.dll

using System;
using System.Linq;

namespace TestProj47
{
    public static partial class Extensions
    {
        public static string FormatForHuman(this Exception exception)
        {
            return string.Join("\n", exception.GetType().GetProperties().Select(property => new
                                     {
                                         property.Name,
                                         Value = property.GetValue(exception, null)
                                     }).Select(x => $"{x.Name} = {x.Value?.ToString() ?? string.Empty}") as string[] ??
                                     new[] {$"<formatting failed>\n{exception}"});
        }
    }
}