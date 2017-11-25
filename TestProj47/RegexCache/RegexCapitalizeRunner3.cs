// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.RegexCapitalizeRunner3
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class RegexCapitalizeRunner3 : RegexRunner
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
      if (this.IsBoundary(runtextpos, runtextbeg, runtextend))
      {
        this.CheckTimeout();
        runstack1[--num3] = runtextpos;
        runtrack1[--num4] = 1;
        this.CheckTimeout();
        if (runtextpos < runtextend)
        {
          var str = runtext;
          var index1 = runtextpos;
          var num6 = 1;
          end = index1 + num6;
          if (RegexRunner.CharInClass(str[index1], "\0\x0002\0a{"))
          {
            this.CheckTimeout();
            var numArray1 = runstack1;
            var index2 = num3;
            var num7 = 1;
            var num8 = index2 + num7;
            var start1 = numArray1[index2];
            this.Capture(1, start1, end);
            int num9;
            runtrack1[num9 = num4 - 1] = start1;
            int num10;
            runtrack1[num10 = num9 - 1] = 2;
            this.CheckTimeout();
            var numArray2 = runstack1;
            var index3 = num8;
            var num11 = 1;
            var num12 = index3 + num11;
            var start2 = numArray2[index3];
            this.Capture(0, start2, end);
            int num13;
            runtrack1[num13 = num10 - 1] = start2;
            runtrack1[num5 = num13 - 1] = 2;
          }
          else
            goto label_5;
        }
        else
          goto label_5;
      }
      else
        goto label_5;
label_4:
      this.CheckTimeout();
      this.runtextpos = end;
      return;
label_5:
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
            goto label_6;
        }
      }
label_6:
      this.CheckTimeout();
      var numArray3 = runtrack2;
      var index4 = num4;
      var num14 = 1;
      num5 = index4 + num14;
      end = numArray3[index4];
      goto label_4;
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
        if (RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\0a{"))
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
      this.runtrackcount = 5;
    }
  }
}
