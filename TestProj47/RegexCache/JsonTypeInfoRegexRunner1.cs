// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.JsonTypeInfoRegexRunner1
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class JsonTypeInfoRegexRunner1 : RegexRunner
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
      var num6 = (num5 = runtextend - runtextpos) + 1;
      while (--num6 > 0)
      {
        if (!RegexRunner.CharInClass(runtext[runtextpos++], "\0\0\x0001d"))
        {
          --runtextpos;
          break;
        }
      }
      if (num5 > num6)
      {
        int num7;
        runtrack[num7 = num4 - 1] = num5 - num6 - 1;
        int num8;
        runtrack[num8 = num7 - 1] = runtextpos - 1;
        runtrack[num4 = num8 - 1] = 2;
      }
      while (true)
      {
        int num7;
        do
        {
          this.CheckTimeout();
          int num8;
          if (8 <= runtextend - runtextpos && (int) runtext[runtextpos] == 34 && ((int) runtext[runtextpos + 1] == 95 && (int) runtext[runtextpos + 2] == 95) && ((int) runtext[runtextpos + 3] == 116 && (int) runtext[runtextpos + 4] == 121 && ((int) runtext[runtextpos + 5] == 112 && (int) runtext[runtextpos + 6] == 101)) && (int) runtext[runtextpos + 7] == 34)
          {
            num8 = runtextpos + 8;
            this.CheckTimeout();
            int num9;
            var num10 = (num9 = runtextend - num8) + 1;
            while (--num10 > 0)
            {
              if (!RegexRunner.CharInClass(runtext[num8++], "\0\0\x0001d"))
              {
                --num8;
                break;
              }
            }
            if (num9 > num10)
            {
              int num11;
              runtrack[num11 = num4 - 1] = num9 - num10 - 1;
              int num12;
              runtrack[num12 = num11 - 1] = num8 - 1;
              runtrack[num4 = num12 - 1] = 3;
            }
          }
          else
            goto label_47;
label_13:
          this.CheckTimeout();
          int num13;
          if (num8 < runtextend)
          {
            var str = runtext;
            var index = num8;
            var num9 = 1;
            num13 = index + num9;
            if ((int) str[index] == 58)
            {
              this.CheckTimeout();
              int num10;
              var num11 = (num10 = runtextend - num13) + 1;
              while (--num11 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[num13++], "\0\0\x0001d"))
                {
                  --num13;
                  break;
                }
              }
              if (num10 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num10 - num11 - 1;
                int num14;
                runtrack[num14 = num12 - 1] = num13 - 1;
                runtrack[num4 = num14 - 1] = 4;
              }
            }
            else
              goto label_47;
          }
          else
            goto label_47;
label_21:
          this.CheckTimeout();
          int num15;
          if (num13 < runtextend)
          {
            var str = runtext;
            var index = num13;
            var num9 = 1;
            num15 = index + num9;
            if ((int) str[index] == 34)
            {
              this.CheckTimeout();
              int num10;
              var num11 = (num10 = runtextend - num15) + 1;
              while (--num11 > 0)
              {
                if ((int) runtext[num15++] == 34)
                {
                  --num15;
                  break;
                }
              }
              if (num10 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num10 - num11 - 1;
                int num14;
                runtrack[num14 = num12 - 1] = num15 - 1;
                runtrack[num4 = num14 - 1] = 5;
              }
            }
            else
              goto label_47;
          }
          else
            goto label_47;
label_29:
          this.CheckTimeout();
          int num16;
          if (num15 < runtextend)
          {
            var str = runtext;
            var index = num15;
            var num9 = 1;
            num16 = index + num9;
            if ((int) str[index] == 34)
            {
              this.CheckTimeout();
              int num10;
              var num11 = (num10 = runtextend - num16) + 1;
              while (--num11 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[num16++], "\0\0\x0001d"))
                {
                  --num16;
                  break;
                }
              }
              if (num10 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num10 - num11 - 1;
                int num14;
                runtrack[num14 = num12 - 1] = num16 - 1;
                runtrack[num4 = num14 - 1] = 6;
              }
            }
            else
              goto label_47;
          }
          else
            goto label_47;
label_37:
          this.CheckTimeout();
          int end;
          if (num16 < runtextend)
          {
            var str = runtext;
            var index = num16;
            var num9 = 1;
            end = index + num9;
            if ((int) str[index] == 44)
            {
              this.CheckTimeout();
              int num10;
              var num11 = (num10 = runtextend - end) + 1;
              while (--num11 > 0)
              {
                if (!RegexRunner.CharInClass(runtext[end++], "\0\0\x0001d"))
                {
                  --end;
                  break;
                }
              }
              if (num10 > num11)
              {
                int num12;
                runtrack[num12 = num4 - 1] = num10 - num11 - 1;
                int num14;
                runtrack[num14 = num12 - 1] = end - 1;
                runtrack[num4 = num14 - 1] = 7;
              }
            }
            else
              goto label_47;
          }
          else
            goto label_47;
label_45:
          this.CheckTimeout();
          var numArray1 = runstack;
          var index1 = num3;
          var num17 = 1;
          var num18 = index1 + num17;
          var start = numArray1[index1];
          this.Capture(0, start, end);
          int num19;
          runtrack[num19 = num4 - 1] = start;
          int num20;
          runtrack[num20 = num19 - 1] = 8;
label_46:
          this.CheckTimeout();
          this.runtextpos = end;
          return;
label_47:
          while (true)
          {
            this.runtrackpos = num4;
            this.runstackpos = num3;
            this.EnsureStorage();
            var runtrackpos2 = this.runtrackpos;
            num3 = this.runstackpos;
            runtrack = this.runtrack;
            runstack = this.runstack;
            var numArray2 = runtrack;
            var index2 = runtrackpos2;
            var num9 = 1;
            num4 = index2 + num9;
            switch (numArray2[index2])
            {
              case 1:
                this.CheckTimeout();
                ++num3;
                continue;
              case 2:
                goto label_50;
              case 3:
                goto label_52;
              case 4:
                goto label_54;
              case 5:
                goto label_56;
              case 6:
                goto label_58;
              case 7:
                goto label_60;
              case 8:
                this.CheckTimeout();
                runstack[--num3] = runtrack[num4++];
                this.Uncapture();
                continue;
              default:
                goto label_48;
            }
          }
label_48:
          this.CheckTimeout();
          var numArray3 = runtrack;
          var index3 = num4;
          var num21 = 1;
          num20 = index3 + num21;
          end = numArray3[index3];
          goto label_46;
label_50:
          this.CheckTimeout();
          var numArray4 = runtrack;
          var index4 = num4;
          var num22 = 1;
          var num23 = index4 + num22;
          runtextpos = numArray4[index4];
          var numArray5 = runtrack;
          var index5 = num23;
          var num24 = 1;
          num4 = index5 + num24;
          num7 = numArray5[index5];
          continue;
label_52:
          this.CheckTimeout();
          var numArray6 = runtrack;
          var index6 = num4;
          var num25 = 1;
          var num26 = index6 + num25;
          num8 = numArray6[index6];
          var numArray7 = runtrack;
          var index7 = num26;
          var num27 = 1;
          num4 = index7 + num27;
          var num28 = numArray7[index7];
          if (num28 > 0)
          {
            int num9;
            runtrack[num9 = num4 - 1] = num28 - 1;
            int num10;
            runtrack[num10 = num9 - 1] = num8 - 1;
            runtrack[num4 = num10 - 1] = 3;
            goto label_13;
          }
          else
            goto label_13;
label_54:
          this.CheckTimeout();
          var numArray8 = runtrack;
          var index8 = num4;
          var num29 = 1;
          var num30 = index8 + num29;
          num13 = numArray8[index8];
          var numArray9 = runtrack;
          var index9 = num30;
          var num31 = 1;
          num4 = index9 + num31;
          var num32 = numArray9[index9];
          if (num32 > 0)
          {
            int num9;
            runtrack[num9 = num4 - 1] = num32 - 1;
            int num10;
            runtrack[num10 = num9 - 1] = num13 - 1;
            runtrack[num4 = num10 - 1] = 4;
            goto label_21;
          }
          else
            goto label_21;
label_56:
          this.CheckTimeout();
          var numArray10 = runtrack;
          var index10 = num4;
          var num33 = 1;
          var num34 = index10 + num33;
          num15 = numArray10[index10];
          var numArray11 = runtrack;
          var index11 = num34;
          var num35 = 1;
          num4 = index11 + num35;
          var num36 = numArray11[index11];
          if (num36 > 0)
          {
            int num9;
            runtrack[num9 = num4 - 1] = num36 - 1;
            int num10;
            runtrack[num10 = num9 - 1] = num15 - 1;
            runtrack[num4 = num10 - 1] = 5;
            goto label_29;
          }
          else
            goto label_29;
label_58:
          this.CheckTimeout();
          var numArray12 = runtrack;
          var index12 = num4;
          var num37 = 1;
          var num38 = index12 + num37;
          num16 = numArray12[index12];
          var numArray13 = runtrack;
          var index13 = num38;
          var num39 = 1;
          num4 = index13 + num39;
          var num40 = numArray13[index13];
          if (num40 > 0)
          {
            int num9;
            runtrack[num9 = num4 - 1] = num40 - 1;
            int num10;
            runtrack[num10 = num9 - 1] = num16 - 1;
            runtrack[num4 = num10 - 1] = 6;
            goto label_37;
          }
          else
            goto label_37;
label_60:
          this.CheckTimeout();
          var numArray14 = runtrack;
          var index14 = num4;
          var num41 = 1;
          var num42 = index14 + num41;
          end = numArray14[index14];
          var numArray15 = runtrack;
          var index15 = num42;
          var num43 = 1;
          num4 = index15 + num43;
          var num44 = numArray15[index15];
          if (num44 > 0)
          {
            int num9;
            runtrack[num9 = num4 - 1] = num44 - 1;
            int num10;
            runtrack[num10 = num9 - 1] = end - 1;
            runtrack[num4 = num10 - 1] = 7;
            goto label_45;
          }
          else
            goto label_45;
        }
        while (num7 <= 0);
        int num45;
        runtrack[num45 = num4 - 1] = num7 - 1;
        int num46;
        runtrack[num46 = num45 - 1] = runtextpos - 1;
        runtrack[num4 = num46 - 1] = 2;
      }
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
        if (RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0002\x0001\"#d"))
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
      this.runtrackcount = 9;
    }
  }
}
