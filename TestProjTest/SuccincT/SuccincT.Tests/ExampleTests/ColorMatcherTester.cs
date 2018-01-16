using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Xunit;
using static System.Console;
using static NUnit.Framework.Assert;
using static HSNXT.SuccincTTests.Examples.ColorMatcher;

namespace HSNXT.SuccincTTests.ExampleTests
{
    public sealed class ColorMatcherTester
    {
        [Fact]
        public void ColorMatcherGeneratesRedGreenBlueCorrectly()
        {
            var oldOut = Out;
            using (var sw = new StringWriter())
            {
                SetOut(sw);

                PrintColorName(Color.Red);
                PrintColorName(Color.Green);
                PrintColorName(Color.Blue);
                
                // https://github.com/nunit/nunit/issues/52
                var s = sw.ToString();
                var s2 = $"Red{Environment.NewLine}Green{Environment.NewLine}Blue{Environment.NewLine}";
                if (s != s2)
                    throw new AssertionFailedException("Was: " + s + " / Should be: " + s2);
            }
        }

        [Fact]
        public void ColorMatcherGeneratesGreenBlueRedCorrectly()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);

                PrintColorName(Color.Green);
                PrintColorName(Color.Blue);
                PrintColorName(Color.Red);
                
                // https://github.com/nunit/nunit/issues/52
                var s = sw.ToString();
                var s2 = $"Green{Environment.NewLine}Blue{Environment.NewLine}Red{Environment.NewLine}";
                if (s != s2)
                    throw new AssertionFailedException("Was: " + s + " / Should be: " + s2);
            }
        }
    }
}