// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.ObjAlphaPattern
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  public class ObjAlphaPattern : Regex
  {
    public ObjAlphaPattern()
    {
      this.pattern = "[^a-zA-Z]";
      this.roptions = RegexOptions.Compiled;
      this.internalMatchTimeout = TimeSpan.FromTicks(-10000L);
      this.factory = (RegexRunnerFactory) new ObjAlphaPatternFactory11();
      this.capsize = 1;
      this.InitializeReferences();
    }

    public ObjAlphaPattern([In] TimeSpan obj0)
      : this()
    {
      Regex.ValidateMatchTimeout(obj0);
      this.internalMatchTimeout = obj0;
    }
  }
}
