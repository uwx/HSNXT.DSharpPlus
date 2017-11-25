// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.IsEmailBigRe
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  public class IsEmailBigRe : Regex
  {
    public IsEmailBigRe()
    {
      this.pattern = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
      this.roptions = RegexOptions.Compiled;
      this.internalMatchTimeout = TimeSpan.FromTicks(-10000L);
      this.factory = (RegexRunnerFactory) new IsEmailBigReFactory12();
      this.capsize = 8;
      this.InitializeReferences();
    }

    public IsEmailBigRe([In] TimeSpan obj0)
      : this()
    {
      Regex.ValidateMatchTimeout(obj0);
      this.internalMatchTimeout = obj0;
    }
  }
}
