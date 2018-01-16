using System;
using NUnit.Framework;
using HSNXT.ComLib.Web.ScriptsSupport;



namespace CommonLibrary.Tests.ScriptsSupportTests
{
       
    [TestFixture]
    public class ScriptsServiceTests
    {

        [Test]
        public void CanRegisterDefault()
        {
            Scripts.AddLocation("head", false);
            Scripts.AddLocation("footer", false);
            Scripts.AddJavascript("jquery.latest", "/scripts/jquery.1.4.2.js");
            Scripts.AddCss("app.css", "/styles/app.css");
            var head = Scripts.ToHtml();

            var expected = "<script src=\"/scripts/jquery.1.4.2.js\" type=\"text/javascript\"></script>" + Environment.NewLine
                         + "<link href=\"/styles/app.css\" rel=\"stylesheet\" type=\"text/css\" />" + Environment.NewLine;

            Assert.AreEqual(head, expected);
        }


        [Test]
        public void CanAddToHead()
        {
            Scripts.AddLocation("head", false);
            Scripts.AddLocation("foot", false);
            Scripts.AddJavascript("jquery.latest", "/scripts/jquery.1.4.2.js", "head");            
            Scripts.AddCss("app.css", "/styles/app.css", "head");

            // Add to foot section.
            Scripts.AddJavascript("comlib.js", "/scripts/comlib.js", "foot");

            // Only get the html for the head section.
            var head = Scripts.ToHtml("head");

            var expected = "<script src=\"/scripts/jquery.1.4.2.js\" type=\"text/javascript\"></script>" + Environment.NewLine
                         + "<link href=\"/styles/app.css\" rel=\"stylesheet\" type=\"text/css\" />" + Environment.NewLine;

            Assert.AreEqual(head, expected);
        }
    }
}
