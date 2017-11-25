// Decompiled with JetBrains decompiler
// Type: HSNXT.RegularExpressions.ToLinkRegexRunner4
// Assembly: RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null
// MVID: 1391ACCA-FC0A-47D6-8630-B4C6A3891591
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexLib.dll

using System.Text.RegularExpressions;

namespace HSNXT.RegularExpressions
{
  internal class ToLinkRegexRunner4 : RegexRunner
  {
    protected override void Go()
    {
      var runtext = this.runtext;
      var runtextstart = this.runtextstart;
      var runtextbeg = this.runtextbeg;
      var runtextend = this.runtextend;
      var end = this.runtextpos;
      var runtrack = this.runtrack;
      var runtrackpos1 = this.runtrackpos;
      var runstack = this.runstack;
      var runstackpos = this.runstackpos;
      this.CheckTimeout();
      int num1;
      runtrack[num1 = runtrackpos1 - 1] = end;
      int num2;
      runtrack[num2 = num1 - 1] = 0;
      this.CheckTimeout();
      int num3;
      runstack[num3 = runstackpos - 1] = end;
      int num4;
      runtrack[num4 = num2 - 1] = 1;
      this.CheckTimeout();
      int num5;
      runstack[num5 = num3 - 1] = end;
      int num6;
      runtrack[num6 = num4 - 1] = 1;
      this.CheckTimeout();
      int num7;
      runstack[num7 = num5 - 1] = -1;
      int num8;
      runstack[num8 = num7 - 1] = 0;
      int num9;
      runtrack[num9 = num6 - 1] = 2;
      this.CheckTimeout();
      int num10;
      int num11;
      int num12;
      int num13;
      int num14;
      int index1;
      int num15;
      while (true)
      {
        this.CheckTimeout();
        var numArray1 = runstack;
        var index2 = num8;
        var num16 = 1;
        var num17 = index2 + num16;
        var num18 = numArray1[index2];
        var numArray2 = runstack;
        var index3 = num17;
        var num19 = 1;
        var num20 = index3 + num19;
        int num21;
        var num22 = num21 = numArray2[index3];
        int num23;
        runtrack[num23 = num9 - 1] = num22;
        var num24 = end;
        if (num21 == num24 && num18 >= 0 || num18 >= 1)
          goto label_14;
        else
          goto label_12;
label_1:
        this.CheckTimeout();
        int num25;
        int num26;
        runstack[num26 = num25 - 1] = end;
        int num27;
        runtrack[num27 = num10 - 1] = 1;
        this.CheckTimeout();
        runstack[num25 = num26 - 1] = end;
        runtrack[num10 = num27 - 1] = 1;
        this.CheckTimeout();
        int num28;
        int num29;
        if (4 <= runtextend - end && (int) runtext[end] == 104 && ((int) runtext[end + 1] == 116 && (int) runtext[end + 2] == 116) && (int) runtext[end + 3] == 112)
        {
          end += 4;
          this.CheckTimeout();
          int num30;
          runstack[num30 = num25 - 1] = -1;
          runstack[num28 = num30 - 1] = 0;
          runtrack[num29 = num10 - 1] = 2;
          this.CheckTimeout();
          goto label_5;
        }
        else
          goto label_51;
label_3:
        this.CheckTimeout();
        runstack[--num25] = end;
        runtrack[--num10] = 1;
        this.CheckTimeout();
        if (end < runtextend && (int) runtext[end++] == 115)
        {
          this.CheckTimeout();
          var numArray3 = runstack;
          var index4 = num25;
          var num30 = 1;
          num28 = index4 + num30;
          var start = numArray3[index4];
          this.Capture(3, start, end);
          int num31;
          runtrack[num31 = num10 - 1] = start;
          runtrack[num29 = num31 - 1] = 3;
        }
        else
          goto label_51;
label_5:
        this.CheckTimeout();
        var numArray4 = runstack;
        var index5 = num28;
        var num32 = 1;
        var num33 = index5 + num32;
        var num34 = numArray4[index5];
        var numArray5 = runstack;
        var index6 = num33;
        var num35 = 1;
        var num36 = index6 + num35;
        int num37;
        var num38 = num37 = numArray5[index6];
        int num39;
        runtrack[num39 = num29 - 1] = num38;
        var num40 = end;
        int num41;
        if ((num37 != num40 || num34 < 0) && num34 < 1)
        {
          int num30;
          runstack[num30 = num36 - 1] = end;
          runstack[num25 = num30 - 1] = num34 + 1;
          runtrack[num10 = num39 - 1] = 4;
          if (num10 <= 116 || num25 <= 87)
          {
            runtrack[--num10] = 5;
            goto label_51;
          }
          else
            goto label_3;
        }
        else
        {
          int num30;
          runtrack[num30 = num39 - 1] = num34;
          runtrack[num41 = num30 - 1] = 6;
        }
label_9:
        this.CheckTimeout();
        var numArray6 = runstack;
        var index7 = num36;
        var num42 = 1;
        num25 = index7 + num42;
        var start1 = numArray6[index7];
        this.Capture(6, start1, end);
        int num43;
        runtrack[num43 = num41 - 1] = start1;
        runtrack[num10 = num43 - 1] = 3;
        this.CheckTimeout();
        if (3 <= runtextend - end && (int) runtext[end] == 58 && ((int) runtext[end + 1] == 47 && (int) runtext[end + 2] == 47))
        {
          end += 3;
          this.CheckTimeout();
          var numArray3 = runstack;
          var index4 = num25;
          var num30 = 1;
          num8 = index4 + num30;
          var start2 = numArray3[index4];
          this.Capture(2, start2, end);
          int num31;
          runtrack[num31 = num10 - 1] = start2;
          runtrack[num9 = num31 - 1] = 3;
          continue;
        }
        goto label_51;
label_12:
        int num44;
        runstack[num44 = num20 - 1] = end;
        runstack[num25 = num44 - 1] = num18 + 1;
        runtrack[num10 = num23 - 1] = 7;
        if (num10 <= 116 || num25 <= 87)
        {
          runtrack[--num10] = 8;
          goto label_51;
        }
        else
          goto label_1;
label_14:
        int num45;
        runtrack[num45 = num23 - 1] = num18;
        int num46;
        runtrack[num46 = num45 - 1] = 6;
label_15:
        this.CheckTimeout();
        runstack[num25 = num20 - 1] = end;
        runtrack[num10 = num46 - 1] = 1;
label_16:
        this.CheckTimeout();
        runstack[--num25] = end;
        runtrack[--num10] = 1;
        this.CheckTimeout();
        if (1 <= runtextend - end)
        {
          ++end;
          var num30 = 1;
          while (RegexRunner.CharInClass(runtext[end - num30--], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
          {
            if (num30 <= 0)
            {
              this.CheckTimeout();
              int num31;
              if ((num31 = runtextend - end) > 0)
              {
                int num47;
                runtrack[num47 = num10 - 1] = num31 - 1;
                int num48;
                runtrack[num48 = num47 - 1] = end;
                runtrack[num10 = num48 - 1] = 9;
                goto label_22;
              }
              else
                goto label_22;
            }
          }
          goto label_51;
        }
        else
          goto label_51;
label_22:
        this.CheckTimeout();
        if (end < runtextend && (int) runtext[end++] == 46)
        {
          this.CheckTimeout();
          if (1 <= runtextend - end)
          {
            ++end;
            var num30 = 1;
            while (RegexRunner.CharInClass(runtext[end - num30--], "\0\0\n\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
            {
              if (num30 <= 0)
              {
                this.CheckTimeout();
                int num31;
                var num47 = (num31 = runtextend - end) + 1;
                while (--num47 > 0)
                {
                  if (!RegexRunner.CharInClass(runtext[end++], "\0\0\n\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
                  {
                    --end;
                    break;
                  }
                }
                if (num31 > num47)
                {
                  int num48;
                  runtrack[num48 = num10 - 1] = num31 - num47 - 1;
                  int num49;
                  runtrack[num49 = num48 - 1] = end - 1;
                  runtrack[num10 = num49 - 1] = 10;
                  goto label_33;
                }
                else
                  goto label_33;
              }
            }
            goto label_51;
          }
          else
            goto label_51;
        }
        else
          goto label_51;
label_33:
        this.CheckTimeout();
        var numArray7 = runstack;
        var index8 = num25;
        var num50 = 1;
        var num51 = index8 + num50;
        var start3 = numArray7[index8];
        this.Capture(4, start3, end);
        int num52;
        runtrack[num52 = num10 - 1] = start3;
        int num53;
        runtrack[num53 = num52 - 1] = 3;
        this.CheckTimeout();
        var numArray8 = runstack;
        var index9 = num51;
        var num54 = 1;
        var num55 = index9 + num54;
        int num56;
        var num57 = num56 = numArray8[index9];
        int num58;
        runtrack[num58 = num53 - 1] = num57;
        var num59 = end;
        int num60;
        if (num56 != num59)
        {
          int num30;
          runtrack[num30 = num58 - 1] = end;
          runstack[num25 = num55 - 1] = end;
          runtrack[num10 = num30 - 1] = 11;
          if (num10 <= 116 || num25 <= 87)
          {
            runtrack[--num10] = 12;
            goto label_51;
          }
          else
            goto label_16;
        }
        else
          runtrack[num60 = num58 - 1] = 13;
label_37:
        this.CheckTimeout();
        int num61;
        runstack[num61 = num55 - 1] = -1;
        int num62;
        runstack[num62 = num61 - 1] = 0;
        int num63;
        runtrack[num63 = num60 - 1] = 2;
        this.CheckTimeout();
        goto label_45;
label_38:
        this.CheckTimeout();
        runstack[--num25] = end;
        int num64;
        runtrack[num64 = num10 - 1] = 1;
        this.CheckTimeout();
        int num65;
        var num66 = (num65 = runtextend - end) + 1;
        while (--num66 > 0)
        {
          if (!RegexRunner.CharInClass(runtext[end++], "\0\x0012\0!\"#'(<=>?[\\]^`a{~\x007F"))
          {
            --end;
            break;
          }
        }
        if (num65 > num66)
        {
          int num30;
          runtrack[num30 = num64 - 1] = num65 - num66 - 1;
          int num31;
          runtrack[num31 = num30 - 1] = end - 1;
          runtrack[num64 = num31 - 1] = 14;
        }
label_44:
        this.CheckTimeout();
        var numArray9 = runstack;
        var index10 = num25;
        var num67 = 1;
        num62 = index10 + num67;
        var start4 = numArray9[index10];
        this.Capture(5, start4, end);
        int num68;
        runtrack[num68 = num64 - 1] = start4;
        runtrack[num63 = num68 - 1] = 3;
label_45:
        this.CheckTimeout();
        var numArray10 = runstack;
        var index11 = num62;
        var num69 = 1;
        var num70 = index11 + num69;
        num11 = numArray10[index11];
        var numArray11 = runstack;
        var index12 = num70;
        var num71 = 1;
        num12 = index12 + num71;
        int num72;
        var num73 = num72 = numArray11[index12];
        runtrack[num13 = num63 - 1] = num73;
        var num74 = end;
        if ((num72 != num74 || num11 < 0) && num11 < 1)
        {
          int num30;
          runstack[num30 = num12 - 1] = end;
          runstack[num25 = num30 - 1] = num11 + 1;
          runtrack[num10 = num13 - 1] = 15;
          if (num10 <= 116 || num25 <= 87)
            runtrack[--num10] = 16;
          else
            goto label_38;
        }
        else
          break;
label_51:
        var num75 = 0;
        while (true)
        {
          var str = "";
          var index4 = 0;
          do
          {
            this.runtrackpos = num10;
            this.runstackpos = num25;
            this.EnsureStorage();
            var runtrackpos2 = this.runtrackpos;
            num25 = this.runstackpos;
            runtrack = this.runtrack;
            runstack = this.runstack;
            var numArray3 = runtrack;
            var index13 = runtrackpos2;
            var num30 = 1;
            num10 = index13 + num30;
            switch (numArray3[index13])
            {
              case 1:
                this.CheckTimeout();
                ++num25;
                continue;
              case 2:
                this.CheckTimeout();
                num25 += 2;
                continue;
              case 3:
                this.CheckTimeout();
                runstack[--num25] = runtrack[num10++];
                this.Uncapture();
                continue;
              case 4:
                this.CheckTimeout();
                var numArray12 = runstack;
                var index14 = num25;
                var num31 = 1;
                var index15 = index14 + num31;
                int num47;
                if ((num47 = numArray12[index14] - 1) >= 0)
                {
                  var numArray13 = runstack;
                  var index16 = index15;
                  var num48 = 1;
                  num36 = index16 + num48;
                  end = numArray13[index16];
                  int num49;
                  runtrack[num49 = num10 - 1] = num47;
                  runtrack[num41 = num49 - 1] = 6;
                  goto label_9;
                }
                else
                {
                  runstack[index15] = runtrack[num10++];
                  runstack[num25 = index15 - 1] = num47;
                  continue;
                }
              case 5:
                goto label_3;
              case 6:
                this.CheckTimeout();
                var numArray14 = runtrack;
                var index17 = num10;
                var num76 = 1;
                var num77 = index17 + num76;
                var num78 = numArray14[index17];
                var numArray15 = runstack;
                int index18;
                var num79 = index18 = num25 - 1;
                var numArray16 = runtrack;
                var index19 = num77;
                var num80 = 1;
                num10 = index19 + num80;
                var num81 = numArray16[index19];
                numArray15[index18] = num81;
                runstack[num25 = num79 - 1] = num78;
                continue;
              case 7:
                this.CheckTimeout();
                var numArray17 = runstack;
                var index20 = num25;
                var num82 = 1;
                var index21 = index20 + num82;
                int num83;
                if ((num83 = numArray17[index20] - 1) >= 0)
                {
                  var numArray13 = runstack;
                  var index16 = index21;
                  var num48 = 1;
                  num20 = index16 + num48;
                  end = numArray13[index16];
                  int num49;
                  runtrack[num49 = num10 - 1] = num83;
                  runtrack[num46 = num49 - 1] = 6;
                  goto label_15;
                }
                else
                {
                  runstack[index21] = runtrack[num10++];
                  runstack[num25 = index21 - 1] = num83;
                  continue;
                }
              case 8:
                goto label_1;
              case 9:
                this.CheckTimeout();
                var numArray18 = runtrack;
                var index22 = num10;
                var num84 = 1;
                var num85 = index22 + num84;
                var num86 = numArray18[index22];
                var numArray19 = runtrack;
                var index23 = num85;
                var num87 = 1;
                num10 = index23 + num87;
                num75 = numArray19[index23];
                str = runtext;
                index4 = num86;
                var num88 = 1;
                end = index4 + num88;
                continue;
              case 10:
                goto label_66;
              case 11:
                goto label_68;
              case 12:
                goto label_16;
              case 13:
                goto label_69;
              case 14:
                goto label_70;
              case 15:
                goto label_72;
              case 16:
                goto label_38;
              default:
                this.CheckTimeout();
                var numArray20 = runtrack;
                var index24 = num10;
                var num89 = 1;
                num14 = index24 + num89;
                end = numArray20[index24];
                goto label_50;
            }
          }
          while (!RegexRunner.CharInClass(str[index4], "\0\x0002\n-.\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"));
          break;
label_69:
          this.CheckTimeout();
          runstack[--num25] = runtrack[num10++];
          continue;
label_72:
          this.CheckTimeout();
          var numArray21 = runstack;
          var index25 = num25;
          var num90 = 1;
          index1 = index25 + num90;
          if ((num15 = numArray21[index25] - 1) < 0)
          {
            runstack[index1] = runtrack[num10++];
            runstack[num25 = index1 - 1] = num15;
          }
          else
            goto label_73;
        }
        if (num75 > 0)
        {
          int num30;
          runtrack[num30 = num10 - 1] = num75 - 1;
          int num31;
          runtrack[num31 = num30 - 1] = end;
          runtrack[num10 = num31 - 1] = 9;
          goto label_22;
        }
        else
          goto label_22;
label_66:
        this.CheckTimeout();
        var numArray22 = runtrack;
        var index26 = num10;
        var num91 = 1;
        var num92 = index26 + num91;
        end = numArray22[index26];
        var numArray23 = runtrack;
        var index27 = num92;
        var num93 = 1;
        num10 = index27 + num93;
        var num94 = numArray23[index27];
        if (num94 > 0)
        {
          int num30;
          runtrack[num30 = num10 - 1] = num94 - 1;
          int num31;
          runtrack[num31 = num30 - 1] = end - 1;
          runtrack[num10 = num31 - 1] = 10;
          goto label_33;
        }
        else
          goto label_33;
label_68:
        this.CheckTimeout();
        var numArray24 = runtrack;
        var index28 = num10;
        var num95 = 1;
        var num96 = index28 + num95;
        end = numArray24[index28];
        var numArray25 = runstack;
        var index29 = num25;
        var num97 = 1;
        num55 = index29 + num97;
        var num98 = numArray25[index29];
        runtrack[num60 = num96 - 1] = 13;
        goto label_37;
label_70:
        this.CheckTimeout();
        var numArray26 = runtrack;
        var index30 = num10;
        var num99 = 1;
        var num100 = index30 + num99;
        end = numArray26[index30];
        var numArray27 = runtrack;
        var index31 = num100;
        var num101 = 1;
        num64 = index31 + num101;
        var num102 = numArray27[index31];
        if (num102 > 0)
        {
          int num30;
          runtrack[num30 = num64 - 1] = num102 - 1;
          int num31;
          runtrack[num31 = num30 - 1] = end - 1;
          runtrack[num64 = num31 - 1] = 14;
          goto label_44;
        }
        else
          goto label_44;
      }
      int num103;
      runtrack[num103 = num13 - 1] = num11;
      int num104;
      runtrack[num104 = num103 - 1] = 6;
label_49:
      this.CheckTimeout();
      var numArray28 = runstack;
      var index32 = num12;
      var num105 = 1;
      var num106 = index32 + num105;
      var start5 = numArray28[index32];
      this.Capture(1, start5, end);
      int num107;
      runtrack[num107 = num104 - 1] = start5;
      int num108;
      runtrack[num108 = num107 - 1] = 3;
      this.CheckTimeout();
      var numArray29 = runstack;
      var index33 = num106;
      var num109 = 1;
      var num110 = index33 + num109;
      var start6 = numArray29[index33];
      this.Capture(0, start6, end);
      int num111;
      runtrack[num111 = num108 - 1] = start6;
      runtrack[num14 = num111 - 1] = 3;
label_50:
      this.CheckTimeout();
      this.runtextpos = end;
      return;
label_73:
      var numArray30 = runstack;
      var index34 = index1;
      var num112 = 1;
      num12 = index34 + num112;
      end = numArray30[index34];
      int num113;
      runtrack[num113 = num10 - 1] = num15;
      runtrack[num104 = num113 - 1] = 6;
      goto label_49;
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
        if (RegexRunner.CharInClass(runtext[runtextpos++], "\0\x0004\n-.hi\0\x0002\x0004\x0005\x0003\x0001\x0006\t\x0013\0"))
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
      this.runtrackcount = 29;
    }
  }
}
