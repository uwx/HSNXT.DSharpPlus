using NUnit.Framework;
using HSNXT;

namespace dff.ExtensionsTests
{
    public class StringExtensionTests
    {
        [Test]
        public void Shorten_String_Works_For_Normal()
        {
            var x = new string('X', 100).ShortenString(10);
            Assert.AreEqual(x, new string('X', 5) + "..." + new string('X', 2));
        }

        [Test]
        public void Shorten_String_Works_For_Length_3()
        {
            var x = new string('X', 3).ShortenString(10);
            Assert.AreEqual(x, x);
        }

        [Test]
        public void Shorten_String_Works_For_Empty_String()
        {
            var x = new string('X', 0).ShortenString(10);
            Assert.AreEqual(x, x);
        }

        [Test]
        public void Shorten_String_Works_For_Null()
        {
            string x;
            x = null;
            x = x.ShortenString(10);
            Assert.AreEqual(x, string.Empty);
        }

        [Test]
        public void Remove_Last_Seperator_Works_For_Null()
        {
            string x = null;
            x = x.RemoveLastSeperator();
            Assert.IsNull(x, x);
        }

        [Test]
        public void Remove_Last_Seperator_Works_For_Empty()
        {
            var x = string.Empty.RemoveLastSeperator();
            Assert.AreEqual(x, string.Empty);
        }

        [Test]
        public void Remove_Last_Seperator_Works_For_Normal()
        {
            var x = (new string('X', 100) + ",").RemoveLastSeperator();
            Assert.AreEqual(new string('X', 100), x);
        }

        [Test]
        public void Remove_Last_Seperator_Works_For_Normal_Wich_Ends_With_Empty_String()
        {
            var x = (new string('X', 100) + ", ").RemoveLastSeperator();
            Assert.AreEqual(new string('X', 100), x);
        }

        [Test]
        public void Remove_Last_Seperator_Works_For_Double_Seperator_Wich_Ends_With_Empty_String()
        {
            var x = (new string('X', 100) + ",, ").RemoveLastSeperator();
            Assert.AreEqual(new string('X', 100), x);
        }

        [Test]
        public void RemoveFirst()
        {
            var x = "Hallo Welt!".RemoveFirst(6);
            Assert.AreEqual("Welt!", x);
        }

        [Test]
        public void RemoveLast()
        {
            var x = "Hallo Welt!".RemoveLast(6);
            Assert.AreEqual("Hallo", x);
        }

        [Test]
        [TestCase("122.333", true)]
        [TestCase("1", true)]
        [TestCase("-122.333", true)]
        [TestCase("-1", true)]
        [TestCase("A", false)]
        [TestCase("-1A", false)]
        [TestCase("1.234A", false)]
        public void IsNumeric_Test(string text, bool result)
        {
            Assert.AreEqual(text.IsNumeric(), result);
        }

        [Test]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("122.333", 0)]
        [TestCase("A", 0)]
        [TestCase("-1A", 0)]
        [TestCase("1.234A", 0)]
        public void TryToInt_Test(string text, int result)
        {
            Assert.AreEqual(text.TryToInt(), result);
        }

        [Test]
        public void RemoveTextBetweenDelimiters()
        {
            var x = "Hallo Welt! <start>Hier der Gruß!!!</start>Hier der Rest.".RemoveTextBetween("<start>", "</start>");
            Assert.AreEqual("Hallo Welt! Hier der Rest.", x);
        }
    }
}
