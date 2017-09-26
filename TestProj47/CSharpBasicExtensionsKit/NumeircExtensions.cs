// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.NumeircExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace TestProj47
{
    public static partial class Extensions
    {
    public static int ToInt(this string value)
    {
      double result = 0.0;
      if (!double.TryParse(value, out result))
        return 0;
      return (int) Math.Round(result);
    }

    public static int? ToIntNullable(this string value)
    {
      double result = 0.0;
      if (!double.TryParse(value, out result))
        return new int?();
      return new int?((int) Math.Round(result));
    }

    public static double? ToDouble(this string value)
    {
      double result = double.MinValue;
      if (!double.TryParse(value, out result))
        return new double?();
      return new double?(result);
    }
  }
}
