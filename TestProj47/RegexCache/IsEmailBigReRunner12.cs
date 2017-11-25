// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.IsEmailBigReRunner12
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class IsEmailBigReRunner12 : RegexRunner
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
        runstack[--num3] = runtextpos;
        runtrack[--num4] = 1;
        this.CheckTimeout();
        if (1 <= runtextend - runtextpos)
        {
          ++runtextpos;
          var num5 = 1;
          while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\n\0-/0:A[_`a{"))
          {
            if (num5 <= 0)
            {
              this.CheckTimeout();
              int num6;
              var num7 = (num6 = runtextend - runtextpos) + 1;
              while (--num7 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\n\0-/0:A[_`a{"))
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
          goto label_104;
        }
        else
          goto label_104;
      }
      else
        goto label_104;
label_11:
      this.CheckTimeout();
      var start1 = runstack[num3++];
      this.Capture(1, start1, runtextpos);
      int num10;
      runtrack[num10 = num4 - 1] = start1;
      runtrack[num4 = num10 - 1] = 3;
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 64)
      {
        this.CheckTimeout();
        int num5;
        runstack[num5 = num3 - 1] = runtextpos;
        int num6;
        runtrack[num6 = num4 - 1] = 1;
        this.CheckTimeout();
        int num7;
        runtrack[num7 = num6 - 1] = runtextpos;
        int num8;
        runtrack[num8 = num7 - 1] = 4;
        this.CheckTimeout();
        runstack[num3 = num5 - 1] = runtextpos;
        runtrack[num4 = num8 - 1] = 1;
        this.CheckTimeout();
        if (runtextpos < runtextend && (int) runtext[runtextpos++] == 91)
        {
          this.CheckTimeout();
          if (1 <= runtextend - runtextpos)
          {
            ++runtextpos;
            var num9 = 1;
            while (RegexRunner.CharInClass(runtext[runtextpos - num9--], "\0\x0002\00:"))
            {
              if (num9 <= 0)
              {
                this.CheckTimeout();
                var num11 = runtextend - runtextpos;
                var num12 = 2;
                if (num11 >= num12)
                  num11 = 2;
                var num13 = num11;
                var num14 = 1;
                var num15 = num11 + num14;
                while (--num15 > 0)
                {
                  if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\00:"))
                  {
                    --runtextpos;
                    break;
                  }
                }
                if (num13 > num15)
                {
                  int num16;
                  runtrack[num16 = num4 - 1] = num13 - num15 - 1;
                  int num17;
                  runtrack[num17 = num16 - 1] = runtextpos - 1;
                  runtrack[num4 = num17 - 1] = 5;
                  goto label_25;
                }
                else
                  goto label_25;
              }
            }
            goto label_104;
          }
          else
            goto label_104;
        }
        else
          goto label_104;
      }
      else
        goto label_104;
label_25:
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
      {
        this.CheckTimeout();
        if (1 <= runtextend - runtextpos)
        {
          ++runtextpos;
          var num5 = 1;
          while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0002\00:"))
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
              var num11 = num6 + num9;
              while (--num11 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\00:"))
                {
                  --runtextpos;
                  break;
                }
              }
              if (num8 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num8 - num11 - 1;
                int num13;
                runtrack[num13 = num12 - 1] = runtextpos - 1;
                runtrack[num4 = num13 - 1] = 6;
                goto label_38;
              }
              else
                goto label_38;
            }
          }
          goto label_104;
        }
        else
          goto label_104;
      }
      else
        goto label_104;
label_38:
      this.CheckTimeout();
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
      {
        this.CheckTimeout();
        if (1 <= runtextend - runtextpos)
        {
          ++runtextpos;
          var num5 = 1;
          while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0002\00:"))
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
              var num11 = num6 + num9;
              while (--num11 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\00:"))
                {
                  --runtextpos;
                  break;
                }
              }
              if (num8 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num8 - num11 - 1;
                int num13;
                runtrack[num13 = num12 - 1] = runtextpos - 1;
                runtrack[num4 = num13 - 1] = 7;
                goto label_51;
              }
              else
                goto label_51;
            }
          }
          goto label_104;
        }
        else
          goto label_104;
      }
      else
        goto label_104;
label_51:
      this.CheckTimeout();
      int num18;
      int num19;
      if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
      {
        this.CheckTimeout();
        var numArray = runstack;
        var index = num3;
        var num5 = 1;
        num18 = index + num5;
        var start2 = numArray[index];
        this.Capture(3, start2, runtextpos);
        int num6;
        runtrack[num6 = num4 - 1] = start2;
        runtrack[num19 = num6 - 1] = 3;
        this.CheckTimeout();
      }
      else
        goto label_104;
label_69:
      this.CheckTimeout();
      var numArray1 = runstack;
      var index1 = num18;
      var num20 = 1;
      var num21 = index1 + num20;
      var start3 = numArray1[index1];
      this.Capture(2, start3, runtextpos);
      int num22;
      runtrack[num22 = num19 - 1] = start3;
      int num23;
      runtrack[num23 = num22 - 1] = 3;
      this.CheckTimeout();
      runstack[num3 = num21 - 1] = runtextpos;
      int num24;
      runtrack[num24 = num23 - 1] = 1;
      this.CheckTimeout();
      int num25;
      runtrack[num25 = num24 - 1] = runtextpos;
      runtrack[num4 = num25 - 1] = 12;
      this.CheckTimeout();
      if (2 <= runtextend - runtextpos)
      {
        runtextpos += 2;
        var num5 = 2;
        while (RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\x0004\0A[a{"))
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
            var num11 = num6 + num9;
            while (--num11 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0004\0A[a{"))
              {
                --runtextpos;
                break;
              }
            }
            if (num8 > num11)
            {
              int num12;
              runtrack[num12 = num4 - 1] = num8 - num11 - 1;
              int num13;
              runtrack[num13 = num12 - 1] = runtextpos - 1;
              runtrack[num4 = num13 - 1] = 13;
              goto label_81;
            }
            else
              goto label_81;
          }
        }
        goto label_104;
      }
      else
        goto label_104;
label_81:
      this.CheckTimeout();
label_93:
      this.CheckTimeout();
      var numArray2 = runstack;
      var index2 = num3;
      var num26 = 1;
      var num27 = index2 + num26;
      var start4 = numArray2[index2];
      this.Capture(6, start4, runtextpos);
      int num28;
      runtrack[num28 = num4 - 1] = start4;
      int num29;
      runtrack[num29 = num28 - 1] = 3;
      this.CheckTimeout();
      runstack[num3 = num27 - 1] = runtextpos;
      int num30;
      runtrack[num30 = num29 - 1] = 1;
      this.CheckTimeout();
      var num31 = runtextend - runtextpos;
      var num32 = 1;
      if (num31 >= num32)
        num31 = 1;
      var num33 = num31;
      var num34 = 1;
      var num35 = num31 + num34;
      while (--num35 > 0)
      {
        if ((int) runtext[runtextpos++] != 93)
        {
          --runtextpos;
          break;
        }
      }
      if (num33 > num35)
      {
        int num5;
        runtrack[num5 = num30 - 1] = num33 - num35 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num30 = num6 - 1] = 15;
      }
label_101:
      this.CheckTimeout();
      var start5 = runstack[num3++];
      this.Capture(7, start5, runtextpos);
      int num36;
      runtrack[num36 = num30 - 1] = start5;
      runtrack[num4 = num36 - 1] = 3;
      this.CheckTimeout();
      int num37;
      if (runtextpos >= runtextend - 1 && (runtextpos >= runtextend || (int) runtext[runtextpos] == 10))
      {
        this.CheckTimeout();
        var numArray3 = runstack;
        var index3 = num3;
        var num5 = 1;
        var num6 = index3 + num5;
        var start2 = numArray3[index3];
        this.Capture(0, start2, runtextpos);
        int num7;
        runtrack[num7 = num4 - 1] = start2;
        runtrack[num37 = num7 - 1] = 3;
      }
      else
        goto label_104;
label_103:
      this.CheckTimeout();
      this.runtextpos = runtextpos;
      return;
label_104:
      int num38;
      int num39;
      int num40;
      while (true)
      {
        do
        {
          this.runtrackpos = num4;
          this.runstackpos = num3;
          this.EnsureStorage();
          var runtrackpos2 = this.runtrackpos;
          num3 = this.runstackpos;
          runtrack = this.runtrack;
          runstack = this.runstack;
          var numArray3 = runtrack;
          var index3 = runtrackpos2;
          var num5 = 1;
          num4 = index3 + num5;
          switch (numArray3[index3])
          {
            case 1:
              this.CheckTimeout();
              ++num3;
              continue;
            case 2:
              this.CheckTimeout();
              var numArray4 = runtrack;
              var index4 = num4;
              var num6 = 1;
              var num7 = index4 + num6;
              runtextpos = numArray4[index4];
              var numArray5 = runtrack;
              var index5 = num7;
              var num8 = 1;
              num4 = index5 + num8;
              var num9 = numArray5[index5];
              if (num9 > 0)
              {
                int num11;
                runtrack[num11 = num4 - 1] = num9 - 1;
                int num12;
                runtrack[num12 = num11 - 1] = runtextpos - 1;
                runtrack[num4 = num12 - 1] = 2;
                goto label_11;
              }
              else
                goto label_11;
            case 3:
              this.CheckTimeout();
              runstack[--num3] = runtrack[num4++];
              this.Uncapture();
              continue;
            case 4:
              this.CheckTimeout();
              var numArray6 = runtrack;
              var index6 = num4;
              var num13 = 1;
              var num14 = index6 + num13;
              runtextpos = numArray6[index6];
              this.CheckTimeout();
              int num15;
              runstack[num15 = num3 - 1] = runtextpos;
              int num16;
              runtrack[num16 = num14 - 1] = 1;
              this.CheckTimeout();
              runstack[num3 = num15 - 1] = runtextpos;
              runtrack[num4 = num16 - 1] = 1;
              goto label_53;
            case 5:
              this.CheckTimeout();
              var numArray7 = runtrack;
              var index7 = num4;
              var num17 = 1;
              var num41 = index7 + num17;
              runtextpos = numArray7[index7];
              var numArray8 = runtrack;
              var index8 = num41;
              var num42 = 1;
              num4 = index8 + num42;
              var num43 = numArray8[index8];
              if (num43 > 0)
              {
                int num11;
                runtrack[num11 = num4 - 1] = num43 - 1;
                int num12;
                runtrack[num12 = num11 - 1] = runtextpos - 1;
                runtrack[num4 = num12 - 1] = 5;
                goto label_25;
              }
              else
                goto label_25;
            case 6:
              this.CheckTimeout();
              var numArray9 = runtrack;
              var index9 = num4;
              var num44 = 1;
              var num45 = index9 + num44;
              runtextpos = numArray9[index9];
              var numArray10 = runtrack;
              var index10 = num45;
              var num46 = 1;
              num4 = index10 + num46;
              var num47 = numArray10[index10];
              if (num47 > 0)
              {
                int num11;
                runtrack[num11 = num4 - 1] = num47 - 1;
                int num12;
                runtrack[num12 = num11 - 1] = runtextpos - 1;
                runtrack[num4 = num12 - 1] = 6;
                goto label_38;
              }
              else
                goto label_38;
            case 7:
              this.CheckTimeout();
              var numArray11 = runtrack;
              var index11 = num4;
              var num48 = 1;
              var num49 = index11 + num48;
              runtextpos = numArray11[index11];
              var numArray12 = runtrack;
              var index12 = num49;
              var num50 = 1;
              num4 = index12 + num50;
              var num51 = numArray12[index12];
              if (num51 > 0)
              {
                int num11;
                runtrack[num11 = num4 - 1] = num51 - 1;
                int num12;
                runtrack[num12 = num11 - 1] = runtextpos - 1;
                runtrack[num4 = num12 - 1] = 7;
                goto label_51;
              }
              else
                goto label_51;
            case 8:
              this.CheckTimeout();
              var numArray13 = runtrack;
              var index13 = num4;
              var num52 = 1;
              var num53 = index13 + num52;
              runtextpos = numArray13[index13];
              var numArray14 = runtrack;
              var index14 = num53;
              var num54 = 1;
              num4 = index14 + num54;
              var num55 = numArray14[index14];
              if (num55 > 0)
              {
                int num11;
                runtrack[num11 = num4 - 1] = num55 - 1;
                int num12;
                runtrack[num12 = num11 - 1] = runtextpos - 1;
                runtrack[num4 = num12 - 1] = 8;
                goto label_63;
              }
              else
                goto label_63;
            case 9:
              this.CheckTimeout();
              var numArray15 = runtrack;
              var index15 = num4;
              var num56 = 1;
              var num57 = index15 + num56;
              runtextpos = numArray15[index15];
              var numArray16 = runstack;
              var index16 = num3;
              var num58 = 1;
              num38 = index16 + num58;
              var num59 = numArray16[index16];
              runtrack[num40 = num57 - 1] = 11;
              goto label_68;
            case 10:
              goto label_53;
            case 11:
              this.CheckTimeout();
              runstack[--num3] = runtrack[num4++];
              continue;
            case 12:
              this.CheckTimeout();
              runtextpos = runtrack[num4++];
              this.CheckTimeout();
              continue;
            case 13:
              goto label_122;
            case 14:
              goto label_124;
            case 15:
              goto label_126;
            default:
              this.CheckTimeout();
              var numArray17 = runtrack;
              var index17 = num4;
              var num60 = 1;
              num37 = index17 + num60;
              runtextpos = numArray17[index17];
              goto label_103;
          }
        }
        while (1 > runtextend - runtextpos);
        goto label_82;
label_53:
        this.CheckTimeout();
        runstack[--num3] = runtextpos;
        runtrack[--num4] = 1;
        this.CheckTimeout();
        if (1 <= runtextend - runtextpos)
        {
          ++runtextpos;
          var num5 = 1;
          do
          {
            if (!RegexRunner.CharInClass(runtext[runtextpos - num5--], "\0\b\0-.0:A[a{"))
              goto label_104;
          }
          while (num5 > 0);
          this.CheckTimeout();
          int num6;
          var num7 = (num6 = runtextend - runtextpos) + 1;
          while (--num7 > 0)
          {
            if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\b\0-.0:A[a{"))
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
            runtrack[num4 = num9 - 1] = 8;
          }
        }
        else
          continue;
label_63:
        this.CheckTimeout();
        if (runtextpos < runtextend && (int) runtext[runtextpos++] == 46)
        {
          this.CheckTimeout();
          var numArray3 = runstack;
          var index3 = num3;
          var num5 = 1;
          var num6 = index3 + num5;
          var start2 = numArray3[index3];
          this.Capture(5, start2, runtextpos);
          int num7;
          runtrack[num7 = num4 - 1] = start2;
          int num8;
          runtrack[num8 = num7 - 1] = 3;
          this.CheckTimeout();
          var numArray4 = runstack;
          var index4 = num6;
          var num9 = 1;
          num38 = index4 + num9;
          int num11;
          var num12 = num11 = numArray4[index4];
          runtrack[num39 = num8 - 1] = num12;
          var num13 = runtextpos;
          if (num11 != num13)
          {
            int num14;
            runtrack[num14 = num39 - 1] = runtextpos;
            runstack[num3 = num38 - 1] = runtextpos;
            runtrack[num4 = num14 - 1] = 9;
            if (num4 <= 124 || num3 <= 93)
              runtrack[--num4] = 10;
            else
              goto label_53;
          }
          else
            break;
        }
      }
      runtrack[num40 = num39 - 1] = 11;
label_68:
      this.CheckTimeout();
      var numArray18 = runstack;
      var index18 = num38;
      var num61 = 1;
      num18 = index18 + num61;
      var start6 = numArray18[index18];
      this.Capture(4, start6, runtextpos);
      int num62;
      runtrack[num62 = num40 - 1] = start6;
      runtrack[num19 = num62 - 1] = 3;
      goto label_69;
label_82:
      ++runtextpos;
      var num63 = 1;
      do
      {
        if (!RegexRunner.CharInClass(runtext[runtextpos - num63--], "\0\x0002\00:"))
          goto label_104;
      }
      while (num63 > 0);
      this.CheckTimeout();
      var num64 = runtextend - runtextpos;
      var num65 = 2;
      if (num64 >= num65)
        num64 = 2;
      var num66 = num64;
      var num67 = 1;
      var num68 = num64 + num67;
      while (--num68 > 0)
      {
        if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\00:"))
        {
          --runtextpos;
          break;
        }
      }
      if (num66 > num68)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num66 - num68 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 14;
        goto label_93;
      }
      else
        goto label_93;
label_122:
      this.CheckTimeout();
      var numArray19 = runtrack;
      var index19 = num4;
      var num69 = 1;
      var num70 = index19 + num69;
      runtextpos = numArray19[index19];
      var numArray20 = runtrack;
      var index20 = num70;
      var num71 = 1;
      num4 = index20 + num71;
      var num72 = numArray20[index20];
      if (num72 > 0)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num72 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 13;
        goto label_81;
      }
      else
        goto label_81;
label_124:
      this.CheckTimeout();
      var numArray21 = runtrack;
      var index21 = num4;
      var num73 = 1;
      var num74 = index21 + num73;
      runtextpos = numArray21[index21];
      var numArray22 = runtrack;
      var index22 = num74;
      var num75 = 1;
      num4 = index22 + num75;
      var num76 = numArray22[index22];
      if (num76 > 0)
      {
        int num5;
        runtrack[num5 = num4 - 1] = num76 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num4 = num6 - 1] = 14;
        goto label_93;
      }
      else
        goto label_93;
label_126:
      this.CheckTimeout();
      var numArray23 = runtrack;
      var index23 = num4;
      var num77 = 1;
      var num78 = index23 + num77;
      runtextpos = numArray23[index23];
      var numArray24 = runtrack;
      var index24 = num78;
      var num79 = 1;
      num30 = index24 + num79;
      var num80 = numArray24[index24];
      if (num80 > 0)
      {
        int num5;
        runtrack[num5 = num30 - 1] = num80 - 1;
        int num6;
        runtrack[num6 = num5 - 1] = runtextpos - 1;
        runtrack[num30 = num6 - 1] = 15;
        goto label_101;
      }
      else
        goto label_101;
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
      this.runtrackcount = 31;
    }
  }
}
