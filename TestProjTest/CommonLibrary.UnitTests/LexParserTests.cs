using System;
using System.Collections.Generic;
using NUnit.Framework;
using HSNXT.ComLib;

namespace CommonLibrary.Tests
{

    [TestFixture]
    public class LexArgsTests
    {

        [Test]
        public void CanParseArguments()
        {
            var args = LexArgs.Parse(" pythontests.py  --config=\"prod.config\"  --trace=4  BLOGS 'c: d: e:'");

            Assert.IsTrue(args.ContainsKey("pythontests.py"));
            Assert.IsTrue(args.ContainsKey("--config=\"prod.config\""));
            Assert.IsTrue(args.ContainsKey("--trace=4"));
            Assert.IsTrue(args.ContainsKey("BLOGS"));
            Assert.IsTrue(args.ContainsKey("c: d: e:"));
            Assert.AreEqual(args.Count, 5);
        }


        [Test]
        public void CanParseArgumentsQuoted()
        {
            var args = LexArgs.Parse(" 'squote' 's\"quote' 's\\'quote' \"dquote\" \"d'quote\" \"d\\\"quote\"");

            Assert.IsTrue(args.ContainsKey("squote"));
            Assert.IsTrue(args.ContainsKey("s\"quote"));
            Assert.IsTrue(args.ContainsKey("s'quote"));
            Assert.IsTrue(args.ContainsKey("dquote"));
            Assert.IsTrue(args.ContainsKey("d'quote"));
            Assert.IsTrue(args.ContainsKey("d\"quote"));
            Assert.AreEqual(args.Count, 6);
        }


        [Test]
        public void CanParseArgumentsWithTabs()
        {
            var args = LexArgs.Parse(" 'squote'	's\"quote'	's\\'quote'");
            
            Assert.IsTrue(args.ContainsKey("squote"));
            Assert.IsTrue(args.ContainsKey("s\"quote"));
            Assert.IsTrue(args.ContainsKey("s'quote"));
            Assert.AreEqual(args.Count, 3);
        }

    }



    [TestFixture]
    public class LexListTests
    {
        [Test]
        public void CanParseNonQuotedListWithCommaSeparator()
        {
            var line = "batman, bruce wayne, gambit";
            var items = LexList.Parse(line);

            Assert.IsTrue(items.ContainsKey("batman"));
            Assert.IsTrue(items.ContainsKey("bruce wayne"));
            Assert.IsTrue(items.ContainsKey("gambit"));
        }


        [Test]
        public void CanParseSingleQuotedListWithCommaSeparator()
        {
            var line = "'batman', 'bruce wayne', 'gambit'";
            var items = LexList.Parse(line);

            Assert.IsTrue(items.ContainsKey("batman"));
            Assert.IsTrue(items.ContainsKey("bruce wayne"));
            Assert.IsTrue(items.ContainsKey("gambit"));
        }


        [Test]
        public void CanParseMultiLineSingleQuotedListWithCommaSeparator()
        {
            var line = "'batman', 'lantern', 'superman' " + Environment.NewLine
                        + "'cyclops', 'colossus', 'logon'";
            var items = LexList.ParseTable(line);

            Assert.IsTrue(items[0].Contains("batman"));
            Assert.IsTrue(items[0].Contains("lantern"));
            Assert.IsTrue(items[0].Contains("superman"));
            Assert.IsTrue(items[1].Contains("cyclops"));
            Assert.IsTrue(items[1].Contains("colossus"));
            Assert.IsTrue(items[1].Contains("logon"));
        }


        [Test]
        public void CanParseSingleQuotedListWithCommaSeparatorWithInnerComma()
        {
            var line = "'batman', 'bruce, wayne', 'gambit'";
            var items = LexList.Parse(line);

            Assert.IsTrue(items.ContainsKey("batman"));
            Assert.IsTrue(items.ContainsKey("bruce, wayne"));
            Assert.IsTrue(items.ContainsKey("gambit"));
        }


        [Test]
        public void CanParseDoubleQuotedListWithCommaSeparatorWithInnerComma()
        {
            var line = "\"batman\", \"bruce, wayne\", \"gambit\"";
            var items = LexList.Parse(line);

            Assert.IsTrue(items.ContainsKey("batman"));
            Assert.IsTrue(items.ContainsKey("bruce, wayne"));
            Assert.IsTrue(items.ContainsKey("gambit"));
        }


        [Test]
        public void CanParseQuotedListNewLine()
        {
            var line = "'batman', 'bruce wayne', 'gambit'\r\n";
            var items = LexList.Parse(line);

            Assert.IsTrue(items.ContainsKey("batman"));
            Assert.IsTrue(items.ContainsKey("bruce wayne"));
            Assert.IsTrue(items.ContainsKey("gambit"));
        }
    }
}
