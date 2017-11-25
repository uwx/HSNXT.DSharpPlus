// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.IsEmailBigReFactory12
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class IsEmailBigReFactory12 : RegexRunnerFactory
  {
    protected override RegexRunner CreateInstance()
    {
      return (RegexRunner) new IsEmailBigReRunner12();
    }
  }
}
