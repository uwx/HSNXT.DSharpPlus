using NUnit.Framework;
using HSNXT.ComLib.Web;


namespace CommonLibrary.Tests
{
    [TestFixture]
    public class UrlTests
    {
        [Test]
        public void CanBuildValidUrlWithOutAnyInvalidChars()
        {
            var title = "This is a post about optimizing a title";
            var url = UrlSeoUtils.BuildValidUrl(title);
            var expected = "this-is-a-post-about-optimizing-a-title";

            Assert.AreEqual(expected, url);
        }


        


        [Test]
        public void CanBuildValidUrlWithInvalidChars()
        {
            var title = "Really~1@# Bad-*7s;:Title";
            var url = UrlSeoUtils.BuildValidUrl(title);
            var expected = "really1-bad-7stitle";

            Assert.AreEqual(expected, url);
        }


        [Test]
        public void CanBuildValidUrlWithSequentialSpaces()
        {
            var title = "Too    many    spaces";
            var url = UrlSeoUtils.BuildValidUrl(title);
            var expected = "too-many-spaces";

            Assert.AreEqual(expected, url);
        }


        [Test]
        public void CanBuildValidUrlWithSequentialDashes()
        {
            var title = "Too---many---dashes";
            var url = UrlSeoUtils.BuildValidUrl(title);
            var expected = "too-many-dashes";

            Assert.AreEqual(expected, url);
        }


        [Test]
        public void CanBuildValidUrlWithInvalidCharsBetweenSpaces()
        {
            var title = "Too ^&* many ;'\"[]\\_+= invalidChars";
            var url = UrlSeoUtils.BuildValidUrl(title);
            var expected = "too-many-invalidchars";

            Assert.AreEqual(expected, url);
        }
    }
}
