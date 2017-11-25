// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.IsEmailRegexRunner2
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class IsEmailRegexRunner2 : RegexRunner
  {
    protected override void Go()
    {
      var runtext = this.runtext;
      var runtextstart = this.runtextstart;
      var runtextbeg = this.runtextbeg;
      var runtextend = this.runtextend;
      var runtextpos = this.runtextpos;
      var runtrack = this.runtrack;
      var runtrackpos1 = this.runtrackpos;
      var runstack = this.runstack;
      var runstackpos = this.runstackpos;
      this.CheckTimeout();
      int num1;
      runtrack[num1 = runtrackpos1 - 1] = runtextpos;
      int num2;
      runtrack[num2 = num1 - 1] = 0;
      this.CheckTimeout();
      int num3;
      runstack[num3 = runstackpos - 1] = runtextpos;
      int num4;
      runtrack[num4 = num2 - 1] = 1;
      this.CheckTimeout();
      if (runtextpos <= runtextbeg)
      {
        this.CheckTimeout();
        if (1 <= runtextend - runtextpos)
        {
          ++runtextpos;
          var num5 = 1;
          while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0002\n-/\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
          {
            if (num5 <= 0)
            {
              this.CheckTimeout();
              int num6;
              var num7 = (num6 = runtextend - runtextpos) + 1;
              while (--num7 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\n-/\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
                {
                  --runtextpos;
                  break;
                }
              }
              if (num6 > num7)
              {
                int num8;
                runtrack[num8 = num4 - 1] = num6 - num7 - 1;
                int num9;
                runtrack[num9 = num8 - 1] = runtextpos - 1;
                runtrack[num4 = num9 - 1] = 2;
                goto label_11;
              }
              else
                goto label_11;
            }
          }
          goto label_43;
        }
        else
          goto label_43;
      }
      else
        goto label_43;
label_11:
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 64)
      {
        this.CheckTimeout();
        runstack[--num3] = runtextpos;
        runtrack[--num4] = 1;
      }
      else
        goto label_43;
label_13:
      this.CheckTimeout();
      runstack[--num3] = runtextpos;
      runtrack[--num4] = 1;
      this.CheckTimeout();
      if (1 <= runtextend - runtextpos)
      {
        ++runtextpos;
        var num5 = 1;
        while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
        {
          if (num5 <= 0)
          {
            this.CheckTimeout();
            int num6;
            var num7 = (num6 = runtextend - runtextpos) + 1;
            while (--num7 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
              {
                --runtextpos;
                break;
              }
            }
            if (num6 > num7)
            {
              int num8;
              runtrack[num8 = num4 - 1] = num6 - num7 - 1;
              int num9;
              runtrack[num9 = num8 - 1] = runtextpos - 1;
              runtrack[num4 = num9 - 1] = 3;
              goto label_23;
            }
            else
              goto label_23;
          }
        }
        goto label_43;
      }
      else
        goto label_43;
label_23:
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
      {
        this.CheckTimeout();
        var numArray1 = runstack;
        var index1 = num3;
        var num5 = 1;
        var num6 = index1 + num5;
        var start = numArray1[index1];
        this.Capture(1, start, runtextpos);
        int num7;
        runtrack[num7 = num4 - 1] = start;
        int num8;
        runtrack[num8 = num7 - 1] = 4;
        this.CheckTimeout();
        var numArray2 = runstack;
        var index2 = num6;
        var num9 = 1;
        num3 = index2 + num9;
        int num10;
        var num11 = num10 = numArray2[index2];
        int num12;
        runtrack[num12 = num8 - 1] = num11;
        var num13 = runtextpos;
        if (num10 != num13)
        {
          int num14;
          runtrack[num14 = num12 - 1] = runtextpos;
          runstack[--num3] = runtextpos;
          runtrack[num4 = num14 - 1] = 5;
          if (num4 <= 40 || num3 <= 30)
          {
            runtrack[--num4] = 6;
            goto label_43;
          }
          else
            goto label_13;
        }
        else
          runtrack[num4 = num12 - 1] = 7;
      }
      else
        goto label_43;
label_28:
      this.CheckTimeout();
      if (2 <= runtextend - runtextpos)
      {
        runtextpos += 2;
        var num5 = 2;
        while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
        {
          if (num5 <= 0)
          {
            this.CheckTimeout();
            var num6 = runtextend - runtextpos;
            var num7 = 2;
            if (num6 >= num7)
              num6 = 2;
            var num8 = num6;
            var num9 = 1;
            var num10 = num6 + num9;
            while (--num10 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
              {
                --runtextpos;
                break;
              }
            }
            if (num8 > num10)
            {
              int num11;
              runtrack[num11 = num4 - 1] = num8 - num10 - 1;
              int num12;
              runtrack[num12 = num11 - 1] = runtextpos - 1;
              runtrack[num4 = num12 - 1] = 8;
              goto label_40;
            }
            else
              goto label_40;
          }
        }
        goto label_43;
      }
      else
        goto label_43;
label_40:
      this.CheckTimeout();
      int num15;
      if (runtextpos >= runtextend - 1 && (runtextpos >= runtextend || (int) runtext[runtextpos] == 10))
      {
        this.CheckTimeout();
        var numArray = runstack;
        var index = num3;
        var num5 = 1;
        var num6 = index + num5;
        var start = numArray[index];
        this.Capture(0, start, runtextpos);
        int num7;
        runtrack[num7 = num4 - 1] = start;
        runtrack[num15 = num7 - 1] = 4;
      }
      else
        goto label_43;
label_42:
      this.CheckTimeout();
      this.runtextpos = runtextpos;
      return;
label_43:
      while (true)
      {
        this.runtrackpos = num4;
        this.runstackpos = num3;
        this.EnsureStorage();
        var runtrackpos2 = this.runtrackpos;
        num3 = this.runstackpos;
        runtrack = this.runtrack;
        runstack = this.runstack;
        var numArray = runtrack;
        var index = runtrackpos2;
        var num5 = 1;
        num4 = index + num5;
        switch (numArray[index])
        {
          case 1:
            this.CheckTimeout();
            ++num3;
            continue;
          case 2:
            goto label_46;
          case 3:
            goto label_48;
          case 4:
            this.CheckTimeout();
            runstack[--num3] = runtrack[num4++];
            this.Uncapture();
            continue;
          case 5:
            goto label_51;
          case 6:
            goto label_13;
          case 7:
            this.CheckTimeout();
            runstack[--num3] = runtrack[num4++];
            continue;
          case 8:
            goto label_53;
          default:
            goto label_44;
        }
      }
label_44:
      this.CheckTimeout();
      var numArray3 = runtrack;
      var index3 = num4;
      var num16 = 1;
      num15 = index3 + num16;
      runtextpos = numArray3[index3];
      goto label_42;
label_46:
      this.CheckTimeout();
      var numArray4 = runtrack;
      var index4 = num4;
      var num17 = 1;
      var num18 = index4 + num17;
      runtextpos = numArray4[index4];
      var numArray5 = runtrack;
      var index5 = num18;
      var num19 = 1;
      num4 = index5 + num19;
      var num20 = numArray5[index5];
      if (num20 > 0)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num20 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 2;
        goto label_11;
      }
      else
        goto label_11;
label_48:
      this.CheckTimeout();
      var numArray6 = runtrack;
      var index6 = num4;
      var num21 = 1;
      var num22 = index6 + num21;
      runtextpos = numArray6[index6];
      var numArray7 = runtrack;
      var index7 = num22;
      var num23 = 1;
      num4 = index7 + num23;
      var num24 = numArray7[index7];
      if (num24 > 0)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num24 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 3;
        goto label_23;
      }
      else
        goto label_23;
label_51:
      this.CheckTimeout();
      var numArray8 = runtrack;
      var index8 = num4;
      var num25 = 1;
      var num26 = index8 + num25;
      runtextpos = numArray8[index8];
      var num27 = runstack[num3++];
      runtrack[num4 = num26 - 1] = 7;
      goto label_28;
label_53:
      this.CheckTimeout();
      var numArray9 = runtrack;
      var index9 = num4;
      var num28 = 1;
      var num29 = index9 + num28;
      runtextpos = numArray9[index9];
      var numArray10 = runtrack;
      var index10 = num29;
      var num30 = 1;
      num4 = index10 + num30;
      var num31 = numArray10[index10];
      if (num31 > 0)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num31 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 8;
        goto label_40;
      }
      else
        goto label_40;
    }

    protected override bool FindFirstChar()
    {
      if (this.runtextpos <= this.runtextbeg)
        return true;
      this.runtextpos = this.runtextend;
      return false;
    }

    protected override void InitTrackCount()
    {
      this.runtrackcount = 10;
    }
  }
}
