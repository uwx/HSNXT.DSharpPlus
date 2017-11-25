// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.ToLinkRegex
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  public class ToLinkRegex : Regex
  {
    public ToLinkRegex()
    {
      this.pattern = "(((?<scheme>http(s)?):\\/\\/)?([\\w-]+?\\.\\w+)+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\,]*)?)";
      this.roptions = RegexOptions.Multiline | RegexOptions.Compiled;
      this.internalMatchTimeout = TimeSpan.FromTicks(-10000L);
      this.factory = (RegexRunnerFactory) new ToLinkRegexFactory4();
      this.capnames = new Hashtable();
      this.capnames.Add((object) "1", (object) 1);
      this.capnames.Add((object) "4", (object) 4);
      this.capnames.Add((object) "5", (object) 5);
      this.capnames.Add((object) "scheme", (object) 6);
      this.capnames.Add((object) "2", (object) 2);
      this.capnames.Add((object) "3", (object) 3);
      this.capnames.Add((object) "0", (object) 0);
      this.capslist = new string[7];
      this.capslist[0] = "0";
      this.capslist[1] = "1";
      this.capslist[2] = "2";
      this.capslist[3] = "3";
      this.capslist[4] = "4";
      this.capslist[5] = "5";
      this.capslist[6] = "scheme";
      this.capsize = 7;
      this.InitializeReferences();
    }

    public ToLinkRegex([In] TimeSpan obj0)
      : this()
    {
      Regex.ValidateMatchTimeout(obj0);
      this.internalMatchTimeout = obj0;
    }
  }
}
