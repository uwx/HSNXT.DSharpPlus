using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSNXT;

namespace ComLib.Test
{
    [TestClass]
    public class NumberExtensionTests
    {
        [TestMethod]
        public void ToFileSizeStringTest()
        {
            // 9.54 MB
            const long size = 0x989680;
            try
            {
                Assert.AreEqual("9.54 MB", size.ToFileSizeString());
            }
            catch
            {
                Assert.AreEqual("9,54 MB", size.ToFileSizeString());
            }
        }
    }
}
