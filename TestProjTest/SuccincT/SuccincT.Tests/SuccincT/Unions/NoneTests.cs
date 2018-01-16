using NUnit.Framework;
using static NUnit.Framework.Assert;
using static HSNXT.SuccincT.Unions.None;

namespace HSNXT.SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class NoneTests
    {
        [Test]
        public void NoneToString_GivesMeanfulResult()
        {
            var value = none;
            AreEqual("!none!", value.ToString());
        }
    }
}
