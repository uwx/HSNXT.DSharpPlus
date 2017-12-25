using System;
using NUnit.Framework;
using HSNXT;

namespace dff.ExtensionsTests
{
    public class TimeSpanExtensionTests
    {
        [Test]
        public void TimeSpan_Works_For_Normal()
        {
            var x = new TimeSpan(12, 12, 12, 12).GetDescription();
            Assert.AreEqual(x, "vor 12 Tagen");
        } 
    }
}