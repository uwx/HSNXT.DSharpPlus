// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.ObjNotWholePatternRunner6
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class ObjNotWholePatternRunner6 : RegexRunner
  {
    protected override void Go()
    {
      var runtext = this.runtext;
      var runtextstart = this.runtextstart;
      var runtextbeg = this.runtextbeg;
      var runtextend = this.runtextend;
      var runtextpos = this.runtextpos;
      var runtrack1 = this.runtrack;
      var runtrackpos1 = this.runtrackpos;
      var runstack1 = this.runstack;
      var runstackpos1 = this.runstackpos;
      this.CheckTimeout();
      int num1;
      runtrack1[num1 = runtrackpos1 - 1] = runtextpos;
      int num2;
      runtrack1[num2 = num1 - 1] = 0;
      this.CheckTimeout();
      int num3;
      runstack1[num3 = runstackpos1 - 1] = runtextpos;
      int num4;
      runtrack1[num4 = num2 - 1] = 1;
      this.CheckTimeout();
      int end;
      int num5;
      if (runtextpos < runtextend)
      {
        var str = runtext;
        var index1 = runtextpos;
        var num6 = 1;
        end = index1 + num6;
        if (RegexRunner.CharInClass(str[index1], "\x0001\x0002\00:"))
        {
          this.CheckTimeout();
          var numArray = runstack1;
          var index2 = num3;
          var num7 = 1;
          var num8 = index2 + num7;
          var start = numArray[index2];
          this.Capture(0, start, end);
          int num9;
          runtrack1[num9 = num4 - 1] = start;
          runtrack1[num5 = num9 - 1] = 2;
        }
        else
          goto label_4;
      }
      else
        goto label_4;
label_3:
      this.CheckTimeout();
      this.runtextpos = end;
      return;
label_4:
      int[] runtrack2;
      while (true)
      {
        this.runtrackpos = num4;
        this.runstackpos = num3;
        this.EnsureStorage();
        var runtrackpos2 = this.runtrackpos;
        var runstackpos2 = this.runstackpos;
        runtrack2 = this.runtrack;
        var runstack2 = this.runstack;
        var numArray = runtrack2;
        var index = runtrackpos2;
        var num6 = 1;
        num4 = index + num6;
        switch (numArray[index])
        {
          case 1:
            this.CheckTimeout();
            num3 = runstackpos2 + 1;
            continue;
          case 2:
            this.CheckTimeout();
            runstack2[num3 = runstackpos2 - 1] = runtrack2[num4++];
            this.Uncapture();
            continue;
          default:
            goto label_5;
        }
      }
label_5:
      this.CheckTimeout();
      var numArray1 = runtrack2;
      var index3 = num4;
      var num10 = 1;
      num5 = index3 + num10;
      end = numArray1[index3];
      goto label_3;
    }

    protected override bool FindFirstChar()
    {
      var runtextpos = this.runtextpos;
      var runtext = this.runtext;
      var num1 = this.runtextend - runtextpos;
      if (num1 <= 0)
        return false;
      do
      {
        --num1;
        if (RegexRunner.CharInClass(runtext[runtextpos++], "\x0001\x0002\00:"))
          goto label_4;
      }
      while (num1 > 0);
      var num2 = 0;
      goto label_5;
label_4:
      --runtextpos;
      num2 = 1;
label_5:
      this.runtextpos = runtextpos;
      return num2 != 0;
    }

    protected override void InitTrackCount()
    {
      this.runtrackcount = 3;
    }
  }
}
