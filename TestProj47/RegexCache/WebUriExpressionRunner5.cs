// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.WebUriExpressionRunner5
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class WebUriExpressionRunner5 : RegexRunner
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
      int num5;
      runstack[num5 = num3 - 1] = runtextpos;
      int num6;
      runtrack[num6 = num4 - 1] = 1;
      this.CheckTimeout();
      int num7;
      runtrack[num7 = num6 - 1] = runtextpos;
      int num8;
      runtrack[num8 = num7 - 1] = 2;
      this.CheckTimeout();
      if (4 <= runtextend - runtextpos && (int) runtext[runtextpos] == 104 && ((int) runtext[runtextpos + 1] == 116 && (int) runtext[runtextpos + 2] == 116) && (int) runtext[runtextpos + 3] == 112)
      {
        runtextpos += 4;
        this.CheckTimeout();
      }
      else
        goto label_45;
label_3:
      this.CheckTimeout();
      var start1 = runstack[num5++];
      this.Capture(1, start1, runtextpos);
      int num9;
      runtrack[num9 = num8 - 1] = start1;
      runtrack[num8 = num9 - 1] = 3;
      this.CheckTimeout();
      if (3 <= runtextend - runtextpos && (int) runtext[runtextpos] == 58 && ((int) runtext[runtextpos + 1] == 47 && (int) runtext[runtextpos + 2] == 47))
      {
        runtextpos += 3;
        this.CheckTimeout();
        runstack[--num5] = runtextpos;
        runtrack[--num8] = 1;
      }
      else
        goto label_45;
label_5:
      this.CheckTimeout();
      runstack[--num5] = runtextpos;
      runtrack[--num8] = 1;
      this.CheckTimeout();
      if (1 <= runtextend - runtextpos)
      {
        ++runtextpos;
        var num10 = 1;
        while (RegexRunner.CharInClass(runtext[runtextpos - num10--], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
        {
          if (num10 <= 0)
          {
            this.CheckTimeout();
            int num11;
            var num12 = (num11 = runtextend - runtextpos) + 1;
            while (--num12 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
              {
                --runtextpos;
                break;
              }
            }
            if (num11 > num12)
            {
              int num13;
              runtrack[num13 = num8 - 1] = num11 - num12 - 1;
              int num14;
              runtrack[num14 = num13 - 1] = runtextpos - 1;
              runtrack[num8 = num14 - 1] = 4;
              goto label_15;
            }
            else
              goto label_15;
          }
        }
        goto label_45;
      }
      else
        goto label_45;
label_15:
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
      {
        this.CheckTimeout();
        var numArray1 = runstack;
        var index1 = num5;
        var num10 = 1;
        var num11 = index1 + num10;
        var start2 = numArray1[index1];
        this.Capture(2, start2, runtextpos);
        int num12;
        runtrack[num12 = num8 - 1] = start2;
        int num13;
        runtrack[num13 = num12 - 1] = 3;
        this.CheckTimeout();
        var numArray2 = runstack;
        var index2 = num11;
        var num14 = 1;
        num5 = index2 + num14;
        int num15;
        var num16 = num15 = numArray2[index2];
        int num17;
        runtrack[num17 = num13 - 1] = num16;
        var num18 = runtextpos;
        if (num15 != num18)
        {
          int num19;
          runtrack[num19 = num17 - 1] = runtextpos;
          runstack[--num5] = runtextpos;
          runtrack[num8 = num19 - 1] = 5;
          if (num8 <= 76 || num5 <= 57)
          {
            runtrack[--num8] = 6;
            goto label_45;
          }
          else
            goto label_5;
        }
        else
          runtrack[num8 = num17 - 1] = 7;
      }
      else
        goto label_45;
label_20:
      this.CheckTimeout();
      if (1 <= runtextend - runtextpos)
      {
        ++runtextpos;
        var num10 = 1;
        while (RegexRunner.CharInClass(runtext[runtextpos - num10--], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
        {
          if (num10 <= 0)
          {
            this.CheckTimeout();
            int num11;
            var num12 = (num11 = runtextend - runtextpos) + 1;
            while (--num12 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
              {
                --runtextpos;
                break;
              }
            }
            if (num11 > num12)
            {
              int num13;
              runtrack[num13 = num8 - 1] = num11 - num12 - 1;
              int num14;
              runtrack[num14 = num13 - 1] = runtextpos - 1;
              runtrack[num8 = num14 - 1] = 8;
              goto label_30;
            }
            else
              goto label_30;
          }
        }
        goto label_45;
      }
      else
        goto label_45;
label_30:
      this.CheckTimeout();
      int num20;
      runstack[num20 = num5 - 1] = -1;
      int num21;
      runstack[num21 = num20 - 1] = 0;
      int num22;
      runtrack[num22 = num8 - 1] = 9;
      this.CheckTimeout();
      goto label_39;
label_31:
      this.CheckTimeout();
      runstack[--num5] = runtextpos;
      runtrack[--num8] = 1;
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 47)
      {
        this.CheckTimeout();
        int num10;
        var num11 = (num10 = runtextend - runtextpos) + 1;
        while (--num11 > 0)
        {
          if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\n\n !%'-0=>?@\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
          {
            --runtextpos;
            break;
          }
        }
        if (num10 > num11)
        {
          int num12;
          runtrack[num12 = num8 - 1] = num10 - num11 - 1;
          int num13;
          runtrack[num13 = num12 - 1] = runtextpos - 1;
          runtrack[num8 = num13 - 1] = 10;
        }
      }
      else
        goto label_45;
label_38:
      this.CheckTimeout();
      var numArray3 = runstack;
      var index3 = num5;
      var num23 = 1;
      num21 = index3 + num23;
      var start3 = numArray3[index3];
      this.Capture(3, start3, runtextpos);
      int num24;
      runtrack[num24 = num8 - 1] = start3;
      runtrack[num22 = num24 - 1] = 3;
label_39:
      this.CheckTimeout();
      var numArray4 = runstack;
      var index4 = num21;
      var num25 = 1;
      var num26 = index4 + num25;
      var num27 = numArray4[index4];
      var numArray5 = runstack;
      var index5 = num26;
      var num28 = 1;
      var num29 = index5 + num28;
      int num30;
      var num31 = num30 = numArray5[index5];
      int num32;
      runtrack[num32 = num22 - 1] = num31;
      var num33 = runtextpos;
      int num34;
      if ((num30 != num33 || num27 < 0) && num27 < 1)
      {
        int num10;
        runstack[num10 = num29 - 1] = runtextpos;
        runstack[num5 = num10 - 1] = num27 + 1;
        runtrack[num8 = num32 - 1] = 11;
        if (num8 <= 76 || num5 <= 57)
        {
          runtrack[--num8] = 12;
          goto label_45;
        }
        else
          goto label_31;
      }
      else
      {
        int num10;
        runtrack[num10 = num32 - 1] = num27;
        runtrack[num34 = num10 - 1] = 13;
      }
label_43:
      this.CheckTimeout();
      var numArray6 = runstack;
      var index6 = num29;
      var num35 = 1;
      var num36 = index6 + num35;
      var start4 = numArray6[index6];
      this.Capture(0, start4, runtextpos);
      int num37;
      runtrack[num37 = num34 - 1] = start4;
      int num38;
      runtrack[num38 = num37 - 1] = 3;
label_44:
      this.CheckTimeout();
      this.runtextpos = runtextpos;
      return;
label_45:
      int index7;
      int num39;
      while (true)
      {
        do
        {
          this.runtrackpos = num8;
          this.runstackpos = num5;
          this.EnsureStorage();
          var runtrackpos2 = this.runtrackpos;
          num5 = this.runstackpos;
          runtrack = this.runtrack;
          runstack = this.runstack;
          var numArray1 = runtrack;
          var index1 = runtrackpos2;
          var num10 = 1;
          num8 = index1 + num10;
          switch (numArray1[index1])
          {
            case 1:
              this.CheckTimeout();
              ++num5;
              continue;
            case 2:
              this.CheckTimeout();
              runtextpos = runtrack[num8++];
              this.CheckTimeout();
              continue;
            case 3:
              goto label_49;
            case 4:
              goto label_50;
            case 5:
              goto label_52;
            case 6:
              goto label_5;
            case 7:
              goto label_53;
            case 8:
              goto label_54;
            case 9:
              goto label_56;
            case 10:
              goto label_57;
            case 11:
              goto label_59;
            case 12:
              goto label_31;
            case 13:
              goto label_62;
            default:
              this.CheckTimeout();
              var numArray2 = runtrack;
              var index2 = num8;
              var num11 = 1;
              num38 = index2 + num11;
              runtextpos = numArray2[index2];
              goto label_44;
          }
        }
        while (5 > runtextend - runtextpos || ((int) runtext[runtextpos] != 104 || (int) runtext[runtextpos + 1] != 116) || ((int) runtext[runtextpos + 2] != 116 || (int) runtext[runtextpos + 3] != 112 || (int) runtext[runtextpos + 4] != 115));
        break;
label_49:
        this.CheckTimeout();
        runstack[--num5] = runtrack[num8++];
        this.Uncapture();
        continue;
label_53:
        this.CheckTimeout();
        runstack[--num5] = runtrack[num8++];
        continue;
label_56:
        this.CheckTimeout();
        num5 += 2;
        continue;
label_59:
        this.CheckTimeout();
        var numArray7 = runstack;
        var index8 = num5;
        var num12 = 1;
        index7 = index8 + num12;
        if ((num39 = numArray7[index8] - 1) < 0)
        {
          runstack[index7] = runtrack[num8++];
          runstack[num5 = index7 - 1] = num39;
          continue;
        }
        goto label_60;
label_62:
        this.CheckTimeout();
        var numArray8 = runtrack;
        var index9 = num8;
        var num13 = 1;
        var num14 = index9 + num13;
        var num15 = numArray8[index9];
        var numArray9 = runstack;
        int index10;
        var num16 = index10 = num5 - 1;
        var numArray10 = runtrack;
        var index11 = num14;
        var num17 = 1;
        num8 = index11 + num17;
        var num18 = numArray10[index11];
        numArray9[index10] = num18;
        runstack[num5 = num16 - 1] = num15;
      }
      runtextpos += 5;
      goto label_3;
label_50:
      this.CheckTimeout();
      var numArray11 = runtrack;
      var index12 = num8;
      var num40 = 1;
      var num41 = index12 + num40;
      runtextpos = numArray11[index12];
      var numArray12 = runtrack;
      var index13 = num41;
      var num42 = 1;
      num8 = index13 + num42;
      var num43 = numArray12[index13];
      if (num43 > 0)
      {
        int num10;
        runtrack[num10 = num8 - 1] = num43 - 1;
        int num11;
        runtrack[num11 = num10 - 1] = runtextpos - 1;
        runtrack[num8 = num11 - 1] = 4;
        goto label_15;
      }
      else
        goto label_15;
label_52:
      this.CheckTimeout();
      var numArray13 = runtrack;
      var index14 = num8;
      var num44 = 1;
      var num45 = index14 + num44;
      runtextpos = numArray13[index14];
      var num46 = runstack[num5++];
      runtrack[num8 = num45 - 1] = 7;
      goto label_20;
label_54:
      this.CheckTimeout();
      var numArray14 = runtrack;
      var index15 = num8;
      var num47 = 1;
      var num48 = index15 + num47;
      runtextpos = numArray14[index15];
      var numArray15 = runtrack;
      var index16 = num48;
      var num49 = 1;
      num8 = index16 + num49;
      var num50 = numArray15[index16];
      if (num50 > 0)
      {
        int num10;
        runtrack[num10 = num8 - 1] = num50 - 1;
        int num11;
        runtrack[num11 = num10 - 1] = runtextpos - 1;
        runtrack[num8 = num11 - 1] = 8;
        goto label_30;
      }
      else
        goto label_30;
label_57:
      this.CheckTimeout();
      var numArray16 = runtrack;
      var index17 = num8;
      var num51 = 1;
      var num52 = index17 + num51;
      runtextpos = numArray16[index17];
      var numArray17 = runtrack;
      var index18 = num52;
      var num53 = 1;
      num8 = index18 + num53;
      var num54 = numArray17[index18];
      if (num54 > 0)
      {
        int num10;
        runtrack[num10 = num8 - 1] = num54 - 1;
        int num11;
        runtrack[num11 = num10 - 1] = runtextpos - 1;
        runtrack[num8 = num11 - 1] = 10;
        goto label_38;
      }
      else
        goto label_38;
label_60:
      var numArray18 = runstack;
      var index19 = index7;
      var num55 = 1;
      num29 = index19 + num55;
      runtextpos = numArray18[index19];
      int num56;
      runtrack[num56 = num8 - 1] = num39;
      runtrack[num34 = num56 - 1] = 13;
      goto label_43;
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
        if ((int) runtext[runtextpos++] == 104)
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
      this.runtrackcount = 19;
    }
  }
}
