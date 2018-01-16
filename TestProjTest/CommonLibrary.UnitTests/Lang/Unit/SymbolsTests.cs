using NUnit.Framework;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Types;

namespace HSNXT.ComLib.Lang.Tests.Unit
{
    [TestFixture]
    public class Symbols_Global_Tests
    {

        [Test]
        public void Can_Define_Variable()
        {
            var syms = new SymbolsGlobal();
            syms.DefineVariable("a");

            Assert.IsTrue(syms.Contains("a"));
        }



        [Test]
        public void Can_Get_Var_Symbol()
        {
            var syms = new SymbolsGlobal();
            syms.DefineVariable("a");
            var sym = syms.GetSymbol("a");

            Assert.AreEqual(sym.Name, "a");
            Assert.AreEqual(sym.DataType, LTypes.Object);            
            Assert.AreEqual(sym.Category, "var");
        }
    }
}